<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        InsertImageBtn = New Button()
        InsertTextBtn = New Button()
        SaveBtn = New Button()
        LayerList = New ListView()
        Label1 = New Label()
        OpenFileDialog1 = New OpenFileDialog()
        SetImagePropsBtn = New Button()
        Label3 = New Label()
        ImageWidthText = New TextBox()
        ImageHeightText = New TextBox()
        Label6 = New Label()
        Label7 = New Label()
        Label2 = New Label()
        Label5 = New Label()
        Label8 = New Label()
        HeightText = New TextBox()
        WidthText = New TextBox()
        SetElementPropsBtn = New Button()
        DeleteLayerBtn = New Button()
        SepiaBtn = New Button()
        GrayscaleBtn = New Button()
        Label4 = New Label()
        BlurBtn = New Button()
        CanvasPanel = New Panel()
        SuspendLayout()
        ' 
        ' InsertImageBtn
        ' 
        InsertImageBtn.Location = New Point(861, 551)
        InsertImageBtn.Name = "InsertImageBtn"
        InsertImageBtn.Size = New Size(160, 29)
        InsertImageBtn.TabIndex = 1
        InsertImageBtn.Text = "Insert Image"
        InsertImageBtn.UseVisualStyleBackColor = True
        ' 
        ' InsertTextBtn
        ' 
        InsertTextBtn.Location = New Point(1038, 551)
        InsertTextBtn.Name = "InsertTextBtn"
        InsertTextBtn.Size = New Size(160, 29)
        InsertTextBtn.TabIndex = 2
        InsertTextBtn.Text = "Insert Text (disabled)"
        InsertTextBtn.UseVisualStyleBackColor = True
        ' 
        ' SaveBtn
        ' 
        SaveBtn.Location = New Point(938, 586)
        SaveBtn.Name = "SaveBtn"
        SaveBtn.Size = New Size(195, 29)
        SaveBtn.TabIndex = 3
        SaveBtn.Text = "Save Image"
        SaveBtn.UseVisualStyleBackColor = True
        ' 
        ' LayerList
        ' 
        LayerList.Location = New Point(886, 53)
        LayerList.Name = "LayerList"
        LayerList.Size = New Size(278, 163)
        LayerList.TabIndex = 4
        LayerList.UseCompatibleStateImageBehavior = False
        LayerList.View = View.List
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(886, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(50, 20)
        Label1.TabIndex = 5
        Label1.Text = "Layers"
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' SetImagePropsBtn
        ' 
        SetImagePropsBtn.Location = New Point(1046, 326)
        SetImagePropsBtn.Name = "SetImagePropsBtn"
        SetImagePropsBtn.Size = New Size(134, 29)
        SetImagePropsBtn.TabIndex = 8
        SetImagePropsBtn.Text = "Set"
        SetImagePropsBtn.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(1045, 229)
        Label3.Name = "Label3"
        Label3.Size = New Size(97, 20)
        Label3.TabIndex = 10
        Label3.Text = "Canvas sizing"
        ' 
        ' ImageWidthText
        ' 
        ImageWidthText.Location = New Point(1110, 260)
        ImageWidthText.Name = "ImageWidthText"
        ImageWidthText.Size = New Size(70, 27)
        ImageWidthText.TabIndex = 13
        ' 
        ' ImageHeightText
        ' 
        ImageHeightText.Location = New Point(1110, 293)
        ImageHeightText.Name = "ImageHeightText"
        ImageHeightText.Size = New Size(70, 27)
        ImageHeightText.TabIndex = 14
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(1045, 293)
        Label6.Name = "Label6"
        Label6.Size = New Size(54, 20)
        Label6.TabIndex = 18
        Label6.Text = "Height"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(1045, 263)
        Label7.Name = "Label7"
        Label7.Size = New Size(49, 20)
        Label7.TabIndex = 17
        Label7.Text = "Width"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(886, 229)
        Label2.Name = "Label2"
        Label2.Size = New Size(93, 20)
        Label2.TabIndex = 19
        Label2.Text = "Image sizing"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(885, 290)
        Label5.Name = "Label5"
        Label5.Size = New Size(54, 20)
        Label5.TabIndex = 23
        Label5.Text = "Height"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(885, 260)
        Label8.Name = "Label8"
        Label8.Size = New Size(49, 20)
        Label8.TabIndex = 22
        Label8.Text = "Width"
        ' 
        ' HeightText
        ' 
        HeightText.Location = New Point(950, 293)
        HeightText.Name = "HeightText"
        HeightText.Size = New Size(71, 27)
        HeightText.TabIndex = 21
        ' 
        ' WidthText
        ' 
        WidthText.Location = New Point(950, 260)
        WidthText.Name = "WidthText"
        WidthText.Size = New Size(71, 27)
        WidthText.TabIndex = 20
        ' 
        ' SetElementPropsBtn
        ' 
        SetElementPropsBtn.Location = New Point(885, 326)
        SetElementPropsBtn.Name = "SetElementPropsBtn"
        SetElementPropsBtn.Size = New Size(136, 26)
        SetElementPropsBtn.TabIndex = 9
        SetElementPropsBtn.Text = "Set"
        SetElementPropsBtn.UseVisualStyleBackColor = True
        ' 
        ' DeleteLayerBtn
        ' 
        DeleteLayerBtn.BackColor = SystemColors.Control
        DeleteLayerBtn.Location = New Point(1037, 21)
        DeleteLayerBtn.Name = "DeleteLayerBtn"
        DeleteLayerBtn.Size = New Size(127, 29)
        DeleteLayerBtn.TabIndex = 24
        DeleteLayerBtn.Text = "Delete Layer"
        DeleteLayerBtn.UseVisualStyleBackColor = False
        ' 
        ' SepiaBtn
        ' 
        SepiaBtn.Location = New Point(884, 440)
        SepiaBtn.Name = "SepiaBtn"
        SepiaBtn.Size = New Size(296, 29)
        SepiaBtn.TabIndex = 25
        SepiaBtn.Text = "Sepia selected"
        SepiaBtn.UseVisualStyleBackColor = True
        ' 
        ' GrayscaleBtn
        ' 
        GrayscaleBtn.Location = New Point(884, 406)
        GrayscaleBtn.Name = "GrayscaleBtn"
        GrayscaleBtn.Size = New Size(145, 26)
        GrayscaleBtn.TabIndex = 27
        GrayscaleBtn.Text = "Grayscale selected"
        GrayscaleBtn.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(884, 382)
        Label4.Name = "Label4"
        Label4.Size = New Size(56, 20)
        Label4.TabIndex = 28
        Label4.Text = "Editing"
        ' 
        ' BlurBtn
        ' 
        BlurBtn.Location = New Point(1035, 405)
        BlurBtn.Name = "BlurBtn"
        BlurBtn.Size = New Size(145, 29)
        BlurBtn.TabIndex = 29
        BlurBtn.Text = "Blur selected"
        BlurBtn.UseVisualStyleBackColor = True
        ' 
        ' CanvasPanel
        ' 
        CanvasPanel.BackColor = Color.White
        CanvasPanel.Location = New Point(37, 27)
        CanvasPanel.Name = "CanvasPanel"
        CanvasPanel.Size = New Size(816, 636)
        CanvasPanel.TabIndex = 30
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1210, 675)
        Controls.Add(CanvasPanel)
        Controls.Add(BlurBtn)
        Controls.Add(Label4)
        Controls.Add(GrayscaleBtn)
        Controls.Add(SepiaBtn)
        Controls.Add(DeleteLayerBtn)
        Controls.Add(SetElementPropsBtn)
        Controls.Add(Label5)
        Controls.Add(Label8)
        Controls.Add(HeightText)
        Controls.Add(WidthText)
        Controls.Add(Label2)
        Controls.Add(Label6)
        Controls.Add(Label7)
        Controls.Add(ImageHeightText)
        Controls.Add(ImageWidthText)
        Controls.Add(Label3)
        Controls.Add(SetImagePropsBtn)
        Controls.Add(Label1)
        Controls.Add(LayerList)
        Controls.Add(SaveBtn)
        Controls.Add(InsertTextBtn)
        Controls.Add(InsertImageBtn)
        Name = "Form1"
        Text = "Image editor"
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents InsertImageBtn As Button
    Friend WithEvents InsertTextBtn As Button
    Friend WithEvents SaveBtn As Button
    Friend WithEvents LayerList As ListView
    Friend WithEvents Label1 As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SetImagePropsBtn As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents ImageWidthText As TextBox
    Friend WithEvents ImageHeightText As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents HeightText As TextBox
    Friend WithEvents WidthText As TextBox
    Friend WithEvents SetElementPropsBtn As Button
    Friend WithEvents DeleteLayerBtn As Button
    Friend WithEvents SepiaBtn As Button
    Friend WithEvents GrayscaleBtn As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents BlurBtn As Button
    Friend WithEvents CanvasPanel As Panel

End Class
