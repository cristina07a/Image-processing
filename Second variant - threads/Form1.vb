Imports System.Drawing.Text
Imports System.Windows.Forms


Imports System.Threading.Tasks
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices


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

                ' fir de executie pentru sepia'
                Dim thread As New Threading.Thread(Sub()
                                                       Dim sepiaImage As Image = ApplySepiaEffect(originalImage)
                                                       ' actualizare imagine cu efect sepia'
                                                       Me.Invoke(Sub()
                                                                     pictureBox.Image = sepiaImage
                                                                     Dim elapsedMilliseconds As Long = stopwatch.ElapsedMilliseconds
                                                                     Time.Text = " " & elapsedMilliseconds
                                                                 End Sub)
                                                   End Sub)
                thread.Start()
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

            'fir de executie pentru blur'
            Dim thread As New Threading.Thread(Sub()
                                                   Dim blurredBmp As Bitmap = GaussianBlur(bmp, blurRadius)
                                                   ' actualizare imagine cu efect blurat'
                                                   Me.Invoke(Sub()
                                                                 pictureBox.Image = blurredBmp
                                                                 Dim elapsedMilliseconds As Long = stopwatch.ElapsedMilliseconds
                                                                     Time.Text = " " & elapsedMilliseconds
                                                             End Sub)
                                               End Sub)
            thread.Start()
        End If
    End Sub

    'GRAYSCALE'
    Private Sub GrayscaleBtn_Click(sender As Object, e As EventArgs) Handles GrayscaleBtn.Click

        If selectedControlFromLayer IsNot Nothing AndAlso TypeOf selectedControlFromLayer Is PictureBox Then
            Dim pictureBox As PictureBox = DirectCast(selectedControlFromLayer, PictureBox)
            Dim bmp As New Bitmap(pictureBox.Image)
            Dim stopwatch As New Stopwatch()
            stopwatch.Start()
            ' fir de executie pentru grayscale'
            Dim thread As New Threading.Thread(Sub()
                                                   Dim grayscaleBmp As Bitmap = GrayscaleImage(bmp)
                                                   Me.Invoke(Sub()
                                                                 ' actualizare imagine cu efect grayscale'
                                                                 pictureBox.Image = grayscaleBmp
                                                                 Dim elapsedMilliseconds As Long = Stopwatch.ElapsedMilliseconds
                                                                 Time.Text = " " & elapsedMilliseconds
                                                             End Sub)
                                               End Sub)
            thread.Start()
        End If
    End Sub

    Private Function GrayscaleImage(image As Bitmap) As Bitmap
        Dim width As Integer = image.Width
        Dim height As Integer = image.Height

        Dim bitmapData As BitmapData = image.LockBits(New Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, image.PixelFormat)

        Dim bytesPerPixel As Integer = image.GetPixelFormatSize(image.PixelFormat) \ 8
        Dim stride As Integer = bitmapData.Stride
        Dim totalBytes As Integer = stride * height
        Dim pixelData(totalBytes - 1) As Byte
        Marshal.Copy(bitmapData.Scan0, pixelData, 0, totalBytes)

        'eliberare imagine din bitmap'
        image.UnlockBits(bitmapData)

        'iteratie in paralel (pe mai multe thread-uri)'
        Parallel.For(0, height,
                Sub(y)
                    For x As Integer = 0 To width - 1
                        Dim offset As Integer = y * stride + x * bytesPerPixel
                        Dim blue As Integer = pixelData(offset)
                        Dim green As Integer = pixelData(offset + 1)
                        Dim red As Integer = pixelData(offset + 2)
                        Dim grayscaleValue As Integer = CInt(0.2125 * red + 0.7154 * green + 0.0721 * blue)

                        ' Set grayscale value for each channel
                        pixelData(offset) = grayscaleValue
                        pixelData(offset + 1) = grayscaleValue
                        pixelData(offset + 2) = grayscaleValue
                    Next
                End Sub)

        Dim grayscaleBitmap As New Bitmap(width, height)
        Dim grayscaleBitmapData As BitmapData = grayscaleBitmap.LockBits(New Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, grayscaleBitmap.PixelFormat)
        Marshal.Copy(pixelData, 0, grayscaleBitmapData.Scan0, totalBytes)
        grayscaleBitmap.UnlockBits(grayscaleBitmapData)

        Return grayscaleBitmap
    End Function

    Private Function ApplySepiaEffect(originalImage As Image) As Image

        Dim sepiaBitmap As New Bitmap(originalImage.Width, originalImage.Height)

        For x As Integer = 0 To originalImage.Width - 1
            For y As Integer = 0 To originalImage.Height - 1
                'culoare pixel original'
                Dim originalColor As Color = DirectCast(originalImage, Bitmap).GetPixel(x, y)

                ' valori sepia'
                Dim sepiaR As Integer = CInt(originalColor.R * 0.393 + originalColor.G * 0.769 + originalColor.B * 0.189)
                Dim sepiaG As Integer = CInt(originalColor.R * 0.349 + originalColor.G * 0.686 + originalColor.B * 0.168)
                Dim sepiaB As Integer = CInt(originalColor.R * 0.272 + originalColor.G * 0.534 + originalColor.B * 0.131)

                sepiaR = Math.Min(255, Math.Max(0, sepiaR))
                sepiaG = Math.Min(255, Math.Max(0, sepiaG))
                sepiaB = Math.Min(255, Math.Max(0, sepiaB))

                ' setare culori'
                Dim sepiaColor As Color = Color.FromArgb(sepiaR, sepiaG, sepiaB)

                sepiaBitmap.SetPixel(x, y, sepiaColor)
            Next
        Next
        Return sepiaBitmap
    End Function

    Private Function GaussianBlur(source As Bitmap, radius As Integer) As Bitmap
        Dim width As Integer = source.Width
        Dim height As Integer = source.Height
        Dim result As New Bitmap(width, height)

        Dim kernelSize As Integer = radius * 2 + 1
        Dim kernel(kernelSize - 1, kernelSize - 1) As Double
        Dim kernelSum As Double = 0

        'calculare kernel'
        For x As Integer = -radius To radius
            For y As Integer = -radius To radius
                Dim distance As Double = Math.Sqrt(x * x + y * y)
                Dim value As Double = Math.Exp(-(distance * distance) / (2 * radius * radius)) / (2 * Math.PI * radius * radius)
                kernel(x + radius, y + radius) = value
                kernelSum += value
            Next
        Next

        ' normalizare kernel '
        For x As Integer = 0 To kernelSize - 1
            For y As Integer = 0 To kernelSize - 1
                kernel(x, y) /= kernelSum
            Next
        Next

        ' aplicare filtru pe imagine'
        For x As Integer = radius To width - 1 - radius
            For y As Integer = radius To height - 1 - radius
                Dim r As Double = 0
                Dim g As Double = 0
                Dim b As Double = 0

                For i As Integer = -radius To radius
                    For j As Integer = -radius To radius
                        Dim pixel As Color = source.GetPixel(x + i, y + j)
                        Dim weight As Double = kernel(i + radius, j + radius)
                        r += pixel.R * weight
                        g += pixel.G * weight
                        b += pixel.B * weight
                    Next
                Next

                r = Math.Min(255, Math.Max(0, r))
                g = Math.Min(255, Math.Max(0, g))
                b = Math.Min(255, Math.Max(0, b))

                result.SetPixel(x, y, Color.FromArgb(CInt(r), CInt(g), CInt(b)))
            Next
        Next

        Return result
    End Function

End Class