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
        Me.lblLogo = New System.Windows.Forms.Label()
        Me.btnHome = New System.Windows.Forms.Button()
        Me.btnMoveCW = New System.Windows.Forms.Button()
        Me.btnMoveCCW = New System.Windows.Forms.Button()
        Me.btnCutElements = New System.Windows.Forms.Button()
        Me.btnDisableMotor = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTraces = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.btnSendTraces = New System.Windows.Forms.Button()
        Me.btnSendQuantity = New System.Windows.Forms.Button()
        Me.btnBladeDown = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblLogo
        '
        Me.lblLogo.AutoSize = True
        Me.lblLogo.Font = New System.Drawing.Font("Microsoft Sans Serif", 28.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogo.Location = New System.Drawing.Point(85, 43)
        Me.lblLogo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLogo.Name = "lblLogo"
        Me.lblLogo.Size = New System.Drawing.Size(468, 55)
        Me.lblLogo.TabIndex = 0
        Me.lblLogo.Text = "CTF Element Cutter"
        '
        'btnHome
        '
        Me.btnHome.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHome.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnHome.Location = New System.Drawing.Point(95, 166)
        Me.btnHome.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnHome.Name = "btnHome"
        Me.btnHome.Size = New System.Drawing.Size(209, 49)
        Me.btnHome.TabIndex = 1
        Me.btnHome.Text = "Home Motor"
        Me.btnHome.UseVisualStyleBackColor = True
        '
        'btnMoveCW
        '
        Me.btnMoveCW.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMoveCW.Location = New System.Drawing.Point(95, 240)
        Me.btnMoveCW.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnMoveCW.Name = "btnMoveCW"
        Me.btnMoveCW.Size = New System.Drawing.Size(209, 49)
        Me.btnMoveCW.TabIndex = 2
        Me.btnMoveCW.Text = "Move ->"
        Me.btnMoveCW.UseVisualStyleBackColor = True
        '
        'btnMoveCCW
        '
        Me.btnMoveCCW.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMoveCCW.Location = New System.Drawing.Point(95, 316)
        Me.btnMoveCCW.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnMoveCCW.Name = "btnMoveCCW"
        Me.btnMoveCCW.Size = New System.Drawing.Size(209, 49)
        Me.btnMoveCCW.TabIndex = 3
        Me.btnMoveCCW.Text = "Move <-"
        Me.btnMoveCCW.UseVisualStyleBackColor = True
        '
        'btnCutElements
        '
        Me.btnCutElements.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCutElements.Location = New System.Drawing.Point(95, 396)
        Me.btnCutElements.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCutElements.Name = "btnCutElements"
        Me.btnCutElements.Size = New System.Drawing.Size(209, 49)
        Me.btnCutElements.TabIndex = 4
        Me.btnCutElements.Text = "Cut Element"
        Me.btnCutElements.UseVisualStyleBackColor = True
        '
        'btnDisableMotor
        '
        Me.btnDisableMotor.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDisableMotor.Location = New System.Drawing.Point(95, 557)
        Me.btnDisableMotor.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnDisableMotor.Name = "btnDisableMotor"
        Me.btnDisableMotor.Size = New System.Drawing.Size(209, 49)
        Me.btnDisableMotor.TabIndex = 5
        Me.btnDisableMotor.Text = "Disable Motor"
        Me.btnDisableMotor.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(565, 166)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(247, 29)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Enter G4 Element Info"
        '
        'txtTraces
        '
        Me.txtTraces.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTraces.Location = New System.Drawing.Point(570, 229)
        Me.txtTraces.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtTraces.Multiline = True
        Me.txtTraces.Name = "txtTraces"
        Me.txtTraces.Size = New System.Drawing.Size(272, 45)
        Me.txtTraces.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(565, 282)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(168, 25)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Number of Traces"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(565, 450)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 25)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Quantity "
        '
        'txtQuantity
        '
        Me.txtQuantity.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuantity.Location = New System.Drawing.Point(570, 401)
        Me.txtQuantity.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtQuantity.Multiline = True
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(272, 45)
        Me.txtQuantity.TabIndex = 9
        '
        'btnSendTraces
        '
        Me.btnSendTraces.Location = New System.Drawing.Point(874, 229)
        Me.btnSendTraces.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSendTraces.Name = "btnSendTraces"
        Me.btnSendTraces.Size = New System.Drawing.Size(153, 56)
        Me.btnSendTraces.TabIndex = 11
        Me.btnSendTraces.Text = "Send Traces"
        Me.btnSendTraces.UseVisualStyleBackColor = True
        '
        'btnSendQuantity
        '
        Me.btnSendQuantity.Location = New System.Drawing.Point(874, 401)
        Me.btnSendQuantity.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSendQuantity.Name = "btnSendQuantity"
        Me.btnSendQuantity.Size = New System.Drawing.Size(153, 56)
        Me.btnSendQuantity.TabIndex = 12
        Me.btnSendQuantity.Text = "Send Quantity"
        Me.btnSendQuantity.UseVisualStyleBackColor = True
        '
        'btnBladeDown
        '
        Me.btnBladeDown.BackColor = System.Drawing.Color.Red
        Me.btnBladeDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBladeDown.Location = New System.Drawing.Point(95, 471)
        Me.btnBladeDown.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnBladeDown.Name = "btnBladeDown"
        Me.btnBladeDown.Size = New System.Drawing.Size(209, 49)
        Me.btnBladeDown.TabIndex = 13
        Me.btnBladeDown.Text = "Blade Down"
        Me.btnBladeDown.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 24)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Label4"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1805, 945)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnBladeDown)
        Me.Controls.Add(Me.btnSendQuantity)
        Me.Controls.Add(Me.btnSendTraces)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtQuantity)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTraces)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnDisableMotor)
        Me.Controls.Add(Me.btnCutElements)
        Me.Controls.Add(Me.btnMoveCCW)
        Me.Controls.Add(Me.btnMoveCW)
        Me.Controls.Add(Me.btnHome)
        Me.Controls.Add(Me.lblLogo)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblLogo As Label
    Friend WithEvents btnHome As Button
    Friend WithEvents btnMoveCW As Button
    Friend WithEvents btnMoveCCW As Button
    Friend WithEvents btnCutElements As Button
    Friend WithEvents btnDisableMotor As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtTraces As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtQuantity As TextBox
    Friend WithEvents btnSendTraces As Button
    Friend WithEvents btnSendQuantity As Button
    Friend WithEvents btnBladeDown As Button
    Friend WithEvents Label4 As Label
End Class
