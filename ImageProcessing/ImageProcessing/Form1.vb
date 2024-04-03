Imports System.Drawing.Text
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

        'Se parcurg toate controalele din CanvasPanel si sunt aduse in fata celorlalte'
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

        'Se parcurg din nou toate controalele din CanvasPanel si sunt aduse in fata celorlalte'
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

    'Private Sub InsertTextBtn_Click(sender As Object, e As EventArgs) Handles InsertTextBtn.Click

    '    Dim text As Label = New Label()

    '    text.Location = Me.PointToClient(New Point(CanvasPanel.Location.X + 500, CanvasPanel.Location.Y + 500))
    '    text.Height = 50
    '    text.Width = 180
    '    text.Text = "Add text"
    '    text.Font = New Font("Arial", 18)
    '    text.ForeColor = Color.Black

    '    AddHandler text.MouseMove, AddressOf Control_Event_MouseMove
    '    AddHandler text.MouseDown, AddressOf Control_Event_MouseDown
    '    AddHandler text.MouseUp, AddressOf Control_Event_MouseUp

    '    CanvasPanel.Controls.Add(text)
    '    text.BringToFront()

    '    AddMyLayerToList(text, "Text")

    '    currentControl = Nothing
    '    dragPoint = Nothing
    'End Sub

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

        'HeightText.Text = selectedControlFromLayer.Height
        'WidthText.Text = selectedControlFromLayer.Width

        'selectedControlFromLayer.BringToFront.

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

    Private Sub SepiaBtn_Click(sender As Object, e As EventArgs) Handles SepiaBtn.Click
        ' verifica daca este selectata o imagine'
        If selectedControlFromLayer IsNot Nothing AndAlso TypeOf selectedControlFromLayer Is PictureBox Then
            Dim pictureBox As PictureBox = CType(selectedControlFromLayer, PictureBox)

            Dim originalImage As Image = pictureBox.Image

            If originalImage IsNot Nothing Then
                ' se aplica efectul sepia'
                Dim sepiaImage As Image = ApplySepiaEffect(originalImage)

                ' se actualizeaza imaginea cu cea sepia
                pictureBox.Image = sepiaImage
            End If
        End If
    End Sub

    Private Function ApplySepiaEffect(originalImage As Image) As Image

        'se creaza un nou bitmap pentru imaginea sepia'
        Dim sepiaBitmap As New Bitmap(originalImage.Width, originalImage.Height)

        'se parcurge fiecare pixel din imagine'
        For x As Integer = 0 To originalImage.Width - 1
            For y As Integer = 0 To originalImage.Height - 1

                ' se obtine culoarea pixelului original'
                Dim originalColor As Color = DirectCast(originalImage, Bitmap).GetPixel(x, y)

                ' calculare valori pentru sepia'
                Dim sepiaR As Integer = CInt(originalColor.R * 0.393 + originalColor.G * 0.769 + originalColor.B * 0.189)
                Dim sepiaG As Integer = CInt(originalColor.R * 0.349 + originalColor.G * 0.686 + originalColor.B * 0.168)
                Dim sepiaB As Integer = CInt(originalColor.R * 0.272 + originalColor.G * 0.534 + originalColor.B * 0.131)

                sepiaR = Math.Min(255, Math.Max(0, sepiaR))
                sepiaG = Math.Min(255, Math.Max(0, sepiaG))
                sepiaB = Math.Min(255, Math.Max(0, sepiaB))

                ' se creaza noua culoare sepia
                Dim sepiaColor As Color = Color.FromArgb(sepiaR, sepiaG, sepiaB)

                sepiaBitmap.SetPixel(x, y, sepiaColor)
            Next
        Next
        Return sepiaBitmap
    End Function

    Private Sub BlurBtn_Click(sender As Object, e As EventArgs) Handles BlurBtn.Click

        'se parcurg toate imaginile din CanvasPanel'
        For Each ctrl As Control In CanvasPanel.Controls
            If TypeOf ctrl Is PictureBox Then
                Dim pictureBox As PictureBox = DirectCast(ctrl, PictureBox)
                Dim bmp As New Bitmap(pictureBox.Image)
                Dim blurRadius As Integer = 5

                Dim blurredBmp As Bitmap = BlurImage(bmp, blurRadius)
                pictureBox.Image = blurredBmp
            End If
        Next

    End Sub

    Private Function BlurImage(image As Bitmap, radius As Integer) As Bitmap
        Dim filter As New AForge.Imaging.Filters.GaussianBlur(radius)
        Return filter.Apply(image)
    End Function

    Private Sub GrayscaleBtn_Click(sender As Object, e As EventArgs) Handles GrayscaleBtn.Click
        For Each ctrl As Control In CanvasPanel.Controls
            If TypeOf ctrl Is PictureBox Then
                Dim pictureBox As PictureBox = DirectCast(ctrl, PictureBox)
                Dim bmp As New Bitmap(pictureBox.Image)

                Dim grayscaleBmp As Bitmap = GrayscaleImage(bmp)
                pictureBox.Image = grayscaleBmp
            End If
        Next
    End Sub

    Private Function GrayscaleImage(image As Bitmap) As Bitmap
        Dim filter As New AForge.Imaging.Filters.Grayscale(0.2125, 0.7154, 0.0721)
        Return filter.Apply(image)
    End Function

End Class
