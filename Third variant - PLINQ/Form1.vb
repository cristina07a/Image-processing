Imports System
Imports System.Collections
Imports System.Diagnostics
Imports System.Drawing.Text
Imports System.Net.Mime.MediaTypeNames
Imports System.Reflection.Emit
Imports System.Windows.Forms

Public Class Form1

    Dim currentControl As Control = Nothing
    Dim dragPoint As Point = Nothing
    Dim layerHashMap As Hashtable = New Hashtable()
    Dim layerNumber As Integer = 1
    Dim selectedControlFromLayer As Control = Nothing
    Dim selectedControlListItem As ListViewItem = Nothing

    'handler pentru apasarea butonului de save'
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click

        'se parcurg toate controalele din CanvasPanel si sunt aduse in fata celorlalte'
        For Each control As Control In CanvasPanel.Controls
            control.BringToFront()
        Next
        CanvasPanel.Refresh()

        'bitmap - array of bits'
        Dim bitmap As Bitmap = New Bitmap(CanvasPanel.Width, CanvasPanel.Height)

        'se stabileste zona din bitmap in care se va desena continutul panoului - 0,0 semnifica'
        'ca incepe din stanga sus si are aceeasi latime si inaltime ca bitmap-ul'
        CanvasPanel.DrawToBitmap(bitmap, New Rectangle(0, 0, bitmap.Width, bitmap.Height))
        bitmap.Save("D:/Facultate/ProiectAPD/image.bmp")

        For Each control As Control In CanvasPanel.Controls
            control.BringToFront()
        Next
        CanvasPanel.Refresh()

    End Sub

    'handler pentru apasarea butonului de insert image'
    Private Sub InsertImageBtn_Click(sender As Object, e As EventArgs) Handles InsertImageBtn.Click

        'afiseaza DialogResult.OK daca a fost adaugata o imagine'
        'sau DialogResult.Cancel daca a anulat comanda de insert image'
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()

        'daca s-a anulat comanda de insert iamge'
        If result <> DialogResult.Cancel Then
            Dim pictureBox As PictureBox = New PictureBox
            pictureBox.Image = Image.FromFile(OpenFileDialog1.FileName) 'incarca imaginea selectata in PictureBox'
            pictureBox.Location = Me.PointToClient(New Point(CanvasPanel.Location.X + 100, CanvasPanel.Location.Y + 100))

            pictureBox.Height = 200
            pictureBox.Width = 200
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage

            AddHandler pictureBox.MouseMove, AddressOf Control_Event_MouseMove
            AddHandler pictureBox.MouseDown, AddressOf Control_Event_MouseDown
            AddHandler pictureBox.MouseUp, AddressOf Control_Event_MouseUp

            CanvasPanel.Controls.Add(pictureBox)
            pictureBox.BringToFront()

            AddMyLayerToList(pictureBox, System.IO.Path.GetFileName(OpenFileDialog1.FileName))

            currentControl = Nothing
            dragPoint = Nothing

        End If

    End Sub

    Private Sub AddMyLayerToList(control As Control, value As String)

        Dim key As String = "Layer " + layerNumber.ToString() + ": " + value
        layerHashMap.Add(key, control)
        LayerList.Items.Add(key)
        layerNumber += 1

    End Sub

    Private Sub Control_Event_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

        If TypeOf sender Is PictureBox Or TypeOf sender Is Label Then
            currentControl = sender
            currentControl.BringToFront()
            dragPoint = New Point(e.X, e.Y)
        End If

    End Sub

    Private Sub Control_Event_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        currentControl = Nothing
        dragPoint = Nothing
    End Sub

    Private Sub Control_Event_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

        If currentControl IsNot Nothing Then
            Dim onMouseCursorPoint As Point = Me.PointToClient(Cursor.Position)
            currentControl.Location = New Point(onMouseCursorPoint.X - dragPoint.X, onMouseCursorPoint.Y - dragPoint.Y)
        End If

    End Sub

    Private Sub SetImagePropsBtn_Click(sender As Object, e As EventArgs) Handles SetImagePropsBtn.Click

        Try
            If ImageHeightText.Text <> "" And ImageWidthText.Text <> "" Then

                Dim height = Double.Parse(ImageHeightText.Text)
                Dim width = Double.Parse(ImageWidthText.Text)
                CanvasPanel.Height = height
                CanvasPanel.Width = width

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        ImageHeightText.Text = CanvasPanel.Height.ToString()
        ImageWidthText.Text = CanvasPanel.Width.ToString()
        Me.BackColor = Color.Ivory
    End Sub

    Private Sub LayerList_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles LayerList.ItemSelectionChanged
        selectedControlFromLayer = CType(layerHashMap.Item(e.Item.Text), Control)
        selectedControlListItem = e.Item
    End Sub

    Private Sub DeleteLayerBtn_Click(sender As Object, e As EventArgs) Handles DeleteLayerBtn.Click

        If selectedControlFromLayer IsNot Nothing Then
            CanvasPanel.Controls.Remove(selectedControlFromLayer)
            LayerList.Items.Remove(selectedControlListItem)
            CanvasPanel.Refresh()
            WidthText.Text = ""
            HeightText.Text = ""
            selectedControlFromLayer = Nothing
            selectedControlListItem = Nothing
        End If

    End Sub

    Private Sub SetElementPropsBtn_Click(sender As Object, e As EventArgs) Handles SetElementPropsBtn.Click

        Try
            If selectedControlFromLayer Is Nothing Then
                Return
            End If

            If HeightText.Text <> "" AndAlso WidthText.Text <> "" AndAlso
                Convert.ToDouble(WidthText.Text) <= CanvasPanel.Width AndAlso
                Convert.ToDouble(HeightText.Text) <= CanvasPanel.Height Then

                Dim height As Double = Double.Parse(HeightText.Text)
                Dim width As Double = Double.Parse(WidthText.Text)

                selectedControlFromLayer.Width = width
                selectedControlFromLayer.Height = height
            End If

        Catch ex As Exception

        End Try
    End Sub

    'SEPIA'
    Private Sub SepiaBtn_Click(sender As Object, e As EventArgs) Handles SepiaBtn.Click

        If selectedControlFromLayer IsNot Nothing AndAlso TypeOf selectedControlFromLayer Is PictureBox Then
            Dim pictureBox As PictureBox = CType(selectedControlFromLayer, PictureBox)

            Dim originalImage As Image = pictureBox.Image

            Dim stopwatch As New Stopwatch()
            stopwatch.Start()

            If originalImage IsNot Nothing Then

                ' folosim PLINQ pentru a itera prin pixelii imaginii și aplicăm efectul sepia
                Dim sepiaImage As Image = ApplySepiaEffect(originalImage)

                ' actualizare imagine cu efect sepia
                pictureBox.Image = sepiaImage
                Dim elapsedMilliseconds As Long = stopwatch.ElapsedMilliseconds
                Time.Text = " " & elapsedMilliseconds
            End If
        End If
    End Sub

    'BLUR CU RADIUS'
    Private Sub BlurBtn_Click(sender As Object, e As EventArgs) Handles BlurBtn.Click

        If selectedControlFromLayer IsNot Nothing AndAlso TypeOf selectedControlFromLayer Is PictureBox Then
            Dim pictureBox As PictureBox = DirectCast(selectedControlFromLayer, PictureBox)
            Dim bmp As New Bitmap(pictureBox.Image)
            Dim blurRadius As Integer = 2

            Dim stopwatch As New Stopwatch()
            stopwatch.Start()

            ' folosim PLINQ pentru a aplica efectul de blur
            Dim blurredBmp As Bitmap = ApplyBlurEffect(bmp, blurRadius)

            ' actualizare imagine cu efect blurat
            pictureBox.Image = blurredBmp
            Dim elapsedMilliseconds As Long = stopwatch.ElapsedMilliseconds
            Time.Text = " " & elapsedMilliseconds
        End If
    End Sub

    'GRAYSCALE'
    Private Sub GrayscaleBtn_Click(sender As Object, e As EventArgs) Handles GrayscaleBtn.Click

        If selectedControlFromLayer IsNot Nothing AndAlso TypeOf selectedControlFromLayer Is PictureBox Then
            Dim pictureBox As PictureBox = DirectCast(selectedControlFromLayer, PictureBox)
            Dim bmp As New Bitmap(pictureBox.Image)
            Dim stopwatch As New Stopwatch()
            stopwatch.Start()

            ' folosim PLINQ pentru a aplica efectul de grayscale
            Dim grayscaleBmp As Bitmap = ApplyGrayscaleEffect(bmp)

            ' actualizare imagine cu efect grayscale
            pictureBox.Image = grayscaleBmp
            Dim elapsedMilliseconds As Long = stopwatch.ElapsedMilliseconds
            Time.Text = " " & elapsedMilliseconds
        End If
    End Sub

    Private Function ApplyGrayscaleEffect(image As Bitmap) As Bitmap
        ' Dacă imaginea este null sau dimensiunea este zero, returnăm direct imaginea
        If image Is Nothing OrElse image.Width = 0 OrElse image.Height = 0 Then
            Return image
        End If

        ' Convertim imaginea la format 32bppArgb pentru a evita problemele cu culoarea
        Dim bitmap As New Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb)
        Using g As Graphics = Graphics.FromImage(bitmap)
            g.DrawImage(image, New Rectangle(0, 0, bitmap.Width, bitmap.Height))
        End Using

        ' Folosim PLINQ pentru a itera prin pixelii imaginii și aplicăm efectul de grayscale
        Dim result = bitmap.AsParallel().Select(Function(pixel)
                                                    Dim gray = CInt(0.2125 * pixel.R + 0.7154 * pixel.G + 0.0721 * pixel.B)
                                                    Return Color.FromArgb(gray, gray, gray)
                                                End Function).ToArray()

        ' Construim o nouă imagine pe baza rezultatului grayscale
        Dim resultBitmap As New Bitmap(image.Width, image.Height)
        For y As Integer = 0 To image.Height - 1
            For x As Integer = 0 To image.Width - 1
                resultBitmap.SetPixel(x, y, result(y * image.Width + x))
            Next
        Next

        Return resultBitmap
    End Function

    Private Function ApplySepiaEffect(image As Bitmap) As Image
        ' Dacă imaginea este null, returnăm direct imaginea
        If image Is Nothing Then
            Return image
        End If

        ' Convertim imaginea la format 32bppArgb pentru a evita problemele cu culoarea
        Dim bitmap As New Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb)
        Using g As Graphics = Graphics.FromImage(bitmap)
            g.DrawImage(image, New Rectangle(0, 0, bitmap.Width, bitmap.Height))
        End Using

        ' Folosim PLINQ pentru a itera prin pixelii imaginii și aplicăm efectul sepia
        Dim result = bitmap.AsParallel().Select(Function(pixel)
                                                    Dim sepiaR As Integer = CInt(pixel.R * 0.393 + pixel.G * 0.769 + pixel.B * 0.189)
                                                    Dim sepiaG As Integer = CInt(pixel.R * 0.349 + pixel.G * 0.686 + pixel.B * 0.168)
                                                    Dim sepiaB As Integer = CInt(pixel.R * 0.272 + pixel.G * 0.534 + pixel.B * 0.131)

                                                    sepiaR = Math.Min(255, Math.Max(0, sepiaR))
                                                    sepiaG = Math.Min(255, Math.Max(0, sepiaG))
                                                    sepiaB = Math.Min(255, Math.Max(0, sepiaB))

                                                    Return Color.FromArgb(sepiaR, sepiaG, sepiaB)
                                                End Function).ToArray()

        ' Construim o nouă imagine pe baza rezultatului sepia
        Dim resultBitmap As New Bitmap(image.Width, image.Height)
        For y As Integer = 0 To image.Height - 1
            For x As Integer = 0 To image.Width - 1
                resultBitmap.SetPixel(x, y, result(y * image.Width + x))
            Next
        Next

        Return resultBitmap
    End Function

    Private Function ApplyBlurEffect(image As Bitmap, radius As Integer) As Bitmap
        ' Dacă imaginea este null, returnăm direct imaginea
        If image Is Nothing Then
            Return image
        End If

        ' Convertim imaginea la format 32bppArgb pentru a evita problemele cu culoarea
        Dim bitmap As New Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb)
        Using g As Graphics = Graphics.FromImage(bitmap)
            g.DrawImage(image, New Rectangle(0, 0, bitmap.Width, bitmap.Height))
        End Using

        ' Folosim PLINQ pentru a itera prin pixelii imaginii și aplicăm efectul de blur
        Dim result = bitmap.AsParallel().Select(Function(pixel)
                                                    Dim r As Double = 0
                                                    Dim g As Double = 0
                                                    Dim b As Double = 0
                                                    Dim kernelSum As Double = 0

                                                    For i As Integer = -radius To radius
                                                        For j As Integer = -radius To radius
                                                            Dim px As Integer = Math.Max(0, Math.Min(bitmap.Width - 1, pixel.X + i))
                                                            Dim py As Integer = Math.Max(0, Math.Min(bitmap.Height - 1, pixel.Y + j))

                                                            ' Extragem valorile RGB ale pixelului
                                                            Dim neighborColor = bitmap.GetPixel(px, py)
                                                            Dim value As Double = Math.Exp(-((i * i + j * j) / (2 * radius * radius))) / (2 * Math.PI * radius * radius)

                                                            r += neighborColor.R * value
                                                            g += neighborColor.G * value
                                                            b += neighborColor.B * value
                                                            kernelSum += value
                                                        Next
                                                    Next

                                                    ' Normalizare culori
                                                    r /= kernelSum
                                                    g /= kernelSum
                                                    b /= kernelSum

                                                    r = Math.Min(255, Math.Max(0, r))
                                                    g = Math.Min(255, Math.Max(0, g))
                                                    b = Math.Min(255, Math.Max(0, b))

                                                    Return Color.FromArgb(CInt(r), CInt(g), CInt(b))
                                                End Function).ToArray()

        ' Construim o nouă imagine pe baza rezultatului blurat
        Dim resultBitmap As New Bitmap(image.Width, image.Height)
        For y As Integer = 0 To image.Height - 1
            For x As Integer = 0 To image.Width - 1
                resultBitmap.SetPixel(x, y, result(y * image.Width + x))
            Next
        Next

        Return resultBitmap
    End Function


End Class
