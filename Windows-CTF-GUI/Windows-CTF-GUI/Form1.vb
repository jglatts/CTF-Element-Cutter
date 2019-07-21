Imports System.IO
Imports System.IO.Ports
Imports System.Threading
' Test out where and what is updating the distance travelled box
' Extract a method for it 
Public Class Form1
    ' globals
    Shared SerialPort1 = New SerialPort()
    Shared distance_travelled As Decimal
    Shared g_traces, motor_steps As Integer
    Shared stepper_data As Byte()
    Shared last_command As String

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
        last_command = "Home Motor"
        writeSerial(b, last_command)
        btnDisableMotor.ForeColor = Color.Black

    End Sub

    Private Sub btnMoveCW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveCW.Click

        Dim b() As Byte = New Byte() {101}

        btnDisableMotor.ForeColor = Color.Black
        ' maybe add a Serial read to see how far we went 
        last_command = "Move ClockWise"
        writeSerial(b, last_command)


    End Sub

    Private Sub btnEmergStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' the arduino is getting 'stuck' trying to keep on resetting
        Dim b() As Byte = New Byte() {117}

        btnDisableMotor.ForeColor = Color.Black
        last_command = "Stop"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnCutElements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCutElements.Click

        Dim b() As Byte = New Byte() {102}

        btnDisableMotor.ForeColor = Color.Black
        last_command = "Cut All Elements"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnDisableMotor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisableMotor.Click

        Dim b() As Byte = New Byte() {103}
        last_command = "Disable Motor"
        writeSerial(b, last_command)
        btnDisableMotor.ForeColor = Color.Red



    End Sub

    Private Sub btnBladeDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBladeDown.Click

        Dim b() As Byte = New Byte() {107}

        ' reset distance travelled
        distance_travelled = 0

        btnDisableMotor.ForeColor = Color.Black

        last_command = "Blade Down"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnMoveCCW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveCCW.Click

        Dim b() As Byte = New Byte() {106}

        btnDisableMotor.ForeColor = Color.Black

        last_command = "Move Counter ClockWise"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnSendTraces_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendTraces.Click

        Dim value As Integer = Convert.ToInt32(txtTraces.Text)  ' grab the number of traces from the textbox and convert to int
        Dim b() As Byte = New Byte() {value}                    ' place the number of traces in a byte array to send through serial

        btnDisableMotor.ForeColor = Color.Black
        g_traces = value
        updateTraceTextBox(value)    ' update distance info

        last_command = "Sent Traces to Machine"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnSendQuantity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendQuantity.Click

        Dim value As Integer
        value = Convert.ToInt32(txtQuantity.Text)
        value += 200
        Dim b() As Byte = New Byte() {value}

        btnDisableMotor.ForeColor = Color.Black

        last_command = "Sent quantity Of G4 elements To cut"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnInchLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInchLeft.Click
        ' move one inch button
        Dim b() As Byte = New Byte() {113}

        btnDisableMotor.ForeColor = Color.Black

        last_command = "Move 1 Inch Left"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnTenthLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTenthLeft.Click

        Dim b() As Byte = New Byte() {114}

        distance_travelled -= 0.1
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        last_command = "Moved 0.1 Inch Left"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnHunLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHunLeft.Click

        Dim b() As Byte = New Byte() {115}

        distance_travelled -= 0.01
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        last_command = "Moved 0.01 Inch Left"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnMilLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMilLeft.Click

        Dim b() As Byte = New Byte() {116}

        distance_travelled -= 0.001
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        last_command = "Moved 0.001 Inch Left"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnInchRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInchRight.Click
        ' move one inch button
        Dim b() As Byte = New Byte() {109}

        ' update the distance travelled textbox
        distance_travelled += 1.0
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        last_command = "Moved 1 Inch Right"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnTenthRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTenthRight.Click

        Dim b() As Byte = New Byte() {110}

        distance_travelled += 0.1
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        last_command = "Moved 0.1 Inch Right"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnHunRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHunRight.Click

        Dim b() As Byte = New Byte() {111}

        distance_travelled += 0.01
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        last_command = "Moved 0.01 Inch Right"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnMilRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMilRight.Click

        Dim b() As Byte = New Byte() {112}

        distance_travelled += 0.001
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        last_command = "Moved 0.001 Inch Right"
        writeSerial(b, last_command)

    End Sub

    Private Sub btnCutMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCutMove.Click
        Dim cut_length As Decimal
        Dim b() As Byte = New Byte() {118}
        Dim m() As Byte = New Byte() {121}  ' add this case to the .ino file

        ' update the text box
        cut_length = ((g_traces + 1) / 2) / 25.4   ' length of the G4 element in inches
        txtCutLength.Text = cut_length
        last_command = "Made cut move for G4"
        writeSerial(b, last_command)
        readSerial()    ' wait for information 

    End Sub

    Private Sub writeSerial(ByVal data As Byte(), ByVal prev_command As String)
        ' send commands to the controller, mega2560, through serial port

        SerialPort1.Open()
        SerialPort1.Write(data, 0, 1)
        SerialPort1.Close()

        ' update Last Command, only when there is one 
        If prev_command <> Nothing Then txtBoxLastCommand.Text = prev_command
        'If prev_command <> "" Then txtBoxLastCommand.Text = prev_command see which one works best 

    End Sub

    Private Sub readSerial()
        ' read data from the serial port
        ' update corresponding GUI elements
        Dim done As Boolean
        Dim inches As Double
        SerialPort1.Open()
        While Not done
            Try
                Dim incoming As String = SerialPort1.ReadExisting()
                ' may not need the top-level if        
                If incoming <> Nothing Then
                    If incoming > 100 Then
                        ' the amount of steps has been sent
                        inches = (incoming / 200) * 0.04
                        txtBoxStepsTaken.Text = incoming
                        txtBoxInchesMoved.Text = inches
                        txtDistTravel.Text = inches
                    Else
                        ' elapsed time has been sent
                        txtBoxMotorTime.Text = incoming
                        done = True
                    End If
                End If
            Catch ex As InvalidOperationException
                MsgBox("Error: Serial Port read timed out.")
            End Try
        End While
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

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click

    End Sub

    Private Sub btnCalibrateMotor_Click(sender As Object, e As EventArgs) Handles btnCalibrateMotor.Click
        Dim b() As Byte = New Byte() {121}
        Dim flag As Boolean
        Dim elap_time As Integer    ' use another structure for the reaminder (i.e, seconds)

        last_command = "Calibrated Motor"
        writeSerial(b, last_command)
        btnDisableMotor.ForeColor = Color.Black

        While Not flag
            Try
                SerialPort1.Open()
                Dim Incoming As String = SerialPort1.ReadExisting()
                SerialPort1.Close() ' may have to move this to the btm
                If Incoming Is Nothing Then
                    MsgBox("nothing" & vbCrLf)
                ElseIf Incoming > 100 Then
                    ' amount of steps has been sent 
                    txtBoxStepsTaken.Text = Incoming
                    txtBoxInchesMoved.Text = Incoming / 200
                    txtDistTravel.Text = Incoming / 200
                ElseIf Incoming < 100 Then
                    ' that ElseIf may not work but fuck it try it anyway 
                    ' elapsed time has been sent 
                    txtBoxMotorTime.Text = Incoming
                    MsgBox("Motor Is Calibrated! Completed in: " & elap_time & " seconds.")
                    flag = True
                Else
                    MsgBox("Motor Moving -- Please close and check again", vbExclamation)
                End If
            Catch ex As InvalidOperationException
                MsgBox("Error: Serial Port read timed out.")
                SerialPort1.Close()
            End Try
        End While
    End Sub

End Class
