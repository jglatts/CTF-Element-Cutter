Imports System.IO
Imports System.IO.Ports
Imports System.Threading

Public Class Form1

    ' globals
    Shared SerialPort1 = New SerialPort()
    Shared distance_travelled As Decimal
    Shared g_traces As Integer
    Shared stepper_data As Byte()

    Private Shared Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' set up serial port
        SerialPort1.PortName = "COM6" ' check And change Arduino port
        SerialPort1.BaudRate = 115200   ' make sure Arduino serial is set at 115200 bps  
        SerialPort1.DataBits = 8
        SerialPort1.Parity = Parity.None
        SerialPort1.StopBits = StopBits.One
        SerialPort1.Handshake = Handshake.None
        SerialPort1.Encoding = System.Text.Encoding.Default ' maybe change to UTF-8, incase any strings are sent
        SerialPort1.ReadTimeout = 1000 ' keep at 1000 -- it's working


        ' hide cut length stuff
        Form1.lblCutLength.Visible = False
        Form1.txtCutLength.Visible = False
        Form1.lblCutLengthInches.Visible = False


    End Sub


    Private Sub btnHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHome.Click

        Dim b() As Byte = New Byte() {100}

        distance_travelled = 0
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)

    End Sub

    Private Sub btnMoveCW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveCW.Click

        Dim b() As Byte = New Byte() {101}

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)


    End Sub

    Private Sub btnEmergStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' the arduino is getting 'stuck' trying to keep on resetting
        Dim b() As Byte = New Byte() {117}

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)

    End Sub

    Private Sub btnCutElements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCutElements.Click

        Dim b() As Byte = New Byte() {102}

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)

    End Sub

    Private Sub btnDisableMotor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisableMotor.Click

        Dim b() As Byte = New Byte() {103}

        btnDisableMotor.ForeColor = Color.Red

        writeSerial(b)

    End Sub

    Private Sub btnBladeDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBladeDown.Click

        Dim b() As Byte = New Byte() {107}

        ' reset distance travelled
        distance_travelled = 0
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)

    End Sub

    Private Sub btnMoveCCW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveCCW.Click

        Dim b() As Byte = New Byte() {106}

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)

    End Sub

    Private Sub btnSendTraces_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendTraces.Click

        Dim value As Integer = Convert.ToInt32(txtTraces.Text)  ' grab the number of traces from the textbox and convert to int
        Dim b() As Byte = New Byte() {value}                    ' place the number of traces in a byte array to send through serial

        btnDisableMotor.ForeColor = Color.Black
        g_traces = value
        updateTraceTextBox(value)    ' update distance info

        writeSerial(b)

    End Sub

    Private Sub btnSendQuantity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendQuantity.Click

        Dim value As Integer
        value = Convert.ToInt32(txtQuantity.Text)
        value += 200
        Dim b() As Byte = New Byte() {value}

        btnDisableMotor.ForeColor = Color.Black

        ' update the progress bar
        cutElementsProgressBar.Maximum = 0  ' reset first
        cutElementsProgressBar.Maximum = Convert.ToInt32(txtQuantity.Text)

        writeSerial(b)

    End Sub

    Private Sub btnInchLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInchLeft.Click
        ' move one inch button
        Dim b() As Byte = New Byte() {113}

        distance_travelled -= 1.0
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)

    End Sub

    Private Sub btnTenthLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTenthLeft.Click

        Dim b() As Byte = New Byte() {114}

        distance_travelled -= 0.1
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)

    End Sub

    Private Sub btnHunLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHunLeft.Click

        Dim b() As Byte = New Byte() {115}

        distance_travelled -= 0.01
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)

    End Sub

    Private Sub btnMilLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMilLeft.Click

        Dim b() As Byte = New Byte() {116}

        distance_travelled -= 0.001
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)

    End Sub

    Private Sub btnInchRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInchRight.Click
        ' move one inch button
        Dim b() As Byte = New Byte() {109}

        ' update the distance travelled textbox
        distance_travelled += 1.0
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)

    End Sub

    Private Sub btnTenthRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTenthRight.Click

        Dim b() As Byte = New Byte() {110}

        distance_travelled += 0.1
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)

    End Sub

    Private Sub btnHunRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHunRight.Click

        Dim b() As Byte = New Byte() {111}

        distance_travelled += 0.01
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)

    End Sub

    Private Sub btnMilRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMilRight.Click

        Dim b() As Byte = New Byte() {112}

        distance_travelled += 0.001
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        writeSerial(b)


    End Sub

    Private Sub btnCutMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCutMove.Click
        Dim cut_length As Decimal
        Dim msg As Integer
        Dim b() As Byte = New Byte() {118}
        Dim m() As Byte = New Byte() {121}  ' add this case to the .ino file

        ' update the text box
        cut_length = ((g_traces + 1) / 2) / 25.4   ' length of the G4 element in inches
        distance_travelled = Decimal.Round(cut_length, 3)
        txtDistTravel.Text = Decimal.Round(cut_length, 3)

        ' update progess bar
        If cutElementsProgressBar.Value < cutElementsProgressBar.Maximum Then
            cutElementsProgressBar.Value += 1
            writeSerial(b)
        Else
            msg = MsgBox(" All elements have been cut " & vbCrLf & " Cut More? ", vbYesNo + vbExclamation)
            Select Case msg ' what button?
                Case 6  ' yes button
                    writeSerial(m)
                    cutElementsProgressBar.Value = 0    ' reset the the quanity value back to 0
                    ' MsgBox("You clicked 'YES' button.")
                Case 7  ' no button
                    ' MsgBox("You clicked 'NO' button.")
            End Select
        End If

    End Sub

    Private Sub writeSerial(ByVal data As Byte())
        ' send commands to the controller, mega2560, through serial port

        SerialPort1.Open()
        SerialPort1.Write(data, 0, 1)
        SerialPort1.Close()

    End Sub

    Private Sub readSerial(ByVal step_data As Byte())
        ' read data from the serial port
        ' test this badboy out

        SerialPort1.Open()
        ' may have to change from byte array to something larger
        ' or maybe it can handle it
        SerialPort1.Read(step_data, 0, 1)
        SerialPort1.Close()

    End Sub

    Private Sub updateTraceTextBox(ByVal traces As Integer)
        ' if the number of traces has been succesfully sent
        ' update the textbox with corresponding length
        Dim cut_length As Decimal

        ' get cut length, in inches
        cut_length = ((traces + 1) / 2) / 25.4
        txtCutLength.Text = Decimal.Round(cut_length, 3)
        lblCutLength.Visible = True
        txtCutLength.Visible = True
        lblCutLengthInches.Visible = True

    End Sub

    Private Sub btnCalibrateMotor_Click(sender As Object, e As EventArgs) Handles btnCalibrateMotor.Click
        Dim b() As Byte = New Byte() {121}
        Dim b_done() As Byte = New Byte() {169}
        Dim flag As Boolean
        Dim Incoming As String

        writeSerial(b)
        btnDisableMotor.ForeColor = Color.Black


        ' Have to tweak the while loop b/c the MsgBox has to be clicked all the time
        ' in order to recieve the "done" string
        ' so close to getting it !
        ' fucking lame -- sometimes work and sometime doesnt WTF
        ' also, this seems like overkill
        While Not flag
            Try
                SerialPort1.Open()
                Incoming = SerialPort1.ReadExisting()
                SerialPort1.Close()
                If Incoming Is Nothing Then
                    MsgBox("nothing" & vbCrLf)
                ElseIf Incoming = "done" Then
                    flag = True
                    MsgBox("Motor is Calibrated and Ready to Go!")
                Else
                    MsgBox("Motor Moving -- Please close and check again")
                End If
            Catch ex As InvalidOperationException
                MsgBox("Error: Serial Port read timed out.")
                SerialPort1.Close()
            End Try
        End While

    End Sub
End Class
