<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.tmr_MouseLock = New System.Windows.Forms.Timer(Me.components)
        Me.tmr_Forward = New System.Windows.Forms.Timer(Me.components)
        Me.tmr_Backward = New System.Windows.Forms.Timer(Me.components)
        Me.tmr_Left = New System.Windows.Forms.Timer(Me.components)
        Me.tmr_Right = New System.Windows.Forms.Timer(Me.components)
        Me.tmr_Upward = New System.Windows.Forms.Timer(Me.components)
        Me.tmr_Downward = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'tmr_MouseLock
        '
        Me.tmr_MouseLock.Enabled = True
        Me.tmr_MouseLock.Interval = 1
        '
        'tmr_Forward
        '
        Me.tmr_Forward.Interval = 20
        '
        'tmr_Backward
        '
        Me.tmr_Backward.Interval = 20
        '
        'tmr_Left
        '
        Me.tmr_Left.Interval = 20
        '
        'tmr_Right
        '
        Me.tmr_Right.Interval = 20
        '
        'tmr_Upward
        '
        Me.tmr_Upward.Interval = 20
        '
        'tmr_Downward
        '
        Me.tmr_Downward.Interval = 20
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Cursor = System.Windows.Forms.Cursors.Cross
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DemoProject"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tmr_MouseLock As Timer
    Friend WithEvents tmr_Forward As Timer
    Friend WithEvents tmr_Backward As Timer
    Friend WithEvents tmr_Left As Timer
    Friend WithEvents tmr_Right As Timer
    Friend WithEvents tmr_Upward As Timer
    Friend WithEvents tmr_Downward As Timer
End Class
