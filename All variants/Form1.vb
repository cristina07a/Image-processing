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


    'ADD HERE THE CODE'














End Class
