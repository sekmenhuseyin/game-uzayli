<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.timer4 = New System.Windows.Forms.Timer(Me.components)
        Me.picturebox_ates = New System.Windows.Forms.PictureBox
        Me.label1 = New System.Windows.Forms.Label
        Me.menuStrip1 = New System.Windows.Forms.MenuStrip
        Me.yeniOyunBaþlatToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.oyuncuÝsmiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox
        Me.oyununZorlukDerecesiToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.kolayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ortaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.zorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.picturebox_ates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.menuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'timer3
        '
        '
        'timer2
        '
        '
        'timer1
        '
        '
        'timer4
        '
        '
        'picturebox_ates
        '
        Me.picturebox_ates.Image = CType(resources.GetObject("picturebox_ates.Image"), System.Drawing.Image)
        Me.picturebox_ates.Location = New System.Drawing.Point(230, 401)
        Me.picturebox_ates.Name = "picturebox_ates"
        Me.picturebox_ates.Size = New System.Drawing.Size(31, 33)
        Me.picturebox_ates.TabIndex = 9
        Me.picturebox_ates.TabStop = False
        '
        'label1
        '
        Me.label1.Image = CType(resources.GetObject("label1.Image"), System.Drawing.Image)
        Me.label1.Location = New System.Drawing.Point(-1, 219)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(34, 38)
        Me.label1.TabIndex = 8
        '
        'menuStrip1
        '
        Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.yeniOyunBaþlatToolStripMenuItem1, Me.oyuncuÝsmiToolStripMenuItem, Me.oyununZorlukDerecesiToolStripMenuItem1})
        Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.menuStrip1.Name = "menuStrip1"
        Me.menuStrip1.Size = New System.Drawing.Size(480, 24)
        Me.menuStrip1.TabIndex = 10
        Me.menuStrip1.Text = "menuStrip1"
        '
        'yeniOyunBaþlatToolStripMenuItem1
        '
        Me.yeniOyunBaþlatToolStripMenuItem1.Name = "yeniOyunBaþlatToolStripMenuItem1"
        Me.yeniOyunBaþlatToolStripMenuItem1.Text = "Yeni Oyun Baþlat"
        '
        'oyuncuÝsmiToolStripMenuItem
        '
        Me.oyuncuÝsmiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripTextBox1})
        Me.oyuncuÝsmiToolStripMenuItem.Name = "oyuncuÝsmiToolStripMenuItem"
        Me.oyuncuÝsmiToolStripMenuItem.Text = "Oyuncu Ýsmi"
        '
        'toolStripTextBox1
        '
        Me.toolStripTextBox1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
        Me.toolStripTextBox1.Name = "toolStripTextBox1"
        Me.toolStripTextBox1.Size = New System.Drawing.Size(100, 21)
        '
        'oyununZorlukDerecesiToolStripMenuItem1
        '
        Me.oyununZorlukDerecesiToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.kolayToolStripMenuItem, Me.ortaToolStripMenuItem, Me.zorToolStripMenuItem})
        Me.oyununZorlukDerecesiToolStripMenuItem1.Name = "oyununZorlukDerecesiToolStripMenuItem1"
        Me.oyununZorlukDerecesiToolStripMenuItem1.Text = "Oyunun Zorluk Derecesi"
        '
        'kolayToolStripMenuItem
        '
        Me.kolayToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.kolayToolStripMenuItem.Name = "kolayToolStripMenuItem"
        Me.kolayToolStripMenuItem.Text = "kolay"
        '
        'ortaToolStripMenuItem
        '
        Me.ortaToolStripMenuItem.Name = "ortaToolStripMenuItem"
        Me.ortaToolStripMenuItem.Text = "orta"
        '
        'zorToolStripMenuItem
        '
        Me.zorToolStripMenuItem.Name = "zorToolStripMenuItem"
        Me.zorToolStripMenuItem.Text = "zor"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 464)
        Me.Controls.Add(Me.picturebox_ates)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.menuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(-1, 219)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.picturebox_ates, System.ComponentModel.ISupportInitialize).EndInit()
        Me.menuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents timer3 As System.Windows.Forms.Timer
    Friend WithEvents timer2 As System.Windows.Forms.Timer
    Friend WithEvents timer1 As System.Windows.Forms.Timer
    Friend WithEvents timer4 As System.Windows.Forms.Timer
    Friend WithEvents picturebox_ates As System.Windows.Forms.PictureBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents menuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents yeniOyunBaþlatToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents oyuncuÝsmiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents oyununZorlukDerecesiToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents kolayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ortaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents zorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
