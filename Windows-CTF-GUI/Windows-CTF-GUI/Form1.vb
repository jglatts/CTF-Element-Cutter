Imports System.IO
Imports System.IO.Ports
Imports System.Threading

Public Class Form1
    ' globals
    Shared show_cut_length As Boolean
    Shared are_traces_sent As Boolean
    Shared is_quantity_sent As Boolean
    Shared g_traces As Integer
    Shared SerialPort1 = New SerialPort()
    Shared distance_travelled As Decimal
    Private Shared Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' set up serial port
        SerialPort1.PortName = "COM6" ' check And change Arduino port
        SerialPort1.BaudRate = 9600
        SerialPort1.DataBits = 8
        SerialPort1.Parity = Parity.None
        SerialPort1.StopBits = StopBits.One
        SerialPort1.Handshake = Handshake.None
        SerialPort1.Encoding = System.Text.Encoding.Default

        ' hide cut length stuff
        Form1.lblCutLength.Visible = False
        Form1.txtCutLength.Visible = False
        Form1.lblCutLengthInches.Visible = False
        show_cut_length = False


    End Sub
    Private Sub btnHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHome.Click

        Dim b() As Byte = New Byte() {100}

        distance_travelled = 0
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnMoveCW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveCW.Click

        Dim b() As Byte = New Byte() {101}

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnCutElements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCutElements.Click

        Dim b() As Byte = New Byte() {102}

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnDisableMotor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisableMotor.Click

        Dim b() As Byte = New Byte() {103}

        btnDisableMotor.ForeColor = Color.Red

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnBladeDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBladeDown.Click

        Dim b() As Byte = New Byte() {107}

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnMoveCCW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveCCW.Click

        Dim b() As Byte = New Byte() {106}

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnSendTraces_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendTraces.Click
        Dim value As Integer
        value = Convert.ToInt32(txtTraces.Text)
        g_traces = value    ' set the global
        Dim b() As Byte = New Byte() {value}

        btnDisableMotor.ForeColor = Color.Black
        are_traces_sent = True  ' set the flag
        checkWhatsBeenSent()    ' check the flags

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub

    Private Sub btnSendQuantity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendQuantity.Click

        Dim value As Integer
        value = Convert.ToInt32(txtQuantity.Text)
        value += 200
        Dim b() As Byte = New Byte() {value}

        btnDisableMotor.ForeColor = Color.Black
        is_quantity_sent = True    ' set the flag
        checkWhatsBeenSent()       ' check the flags

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnInchLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInchLeft.Click
        ' move one inch button
        Dim b() As Byte = New Byte() {113}

        distance_travelled -= 1.0
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnTenthLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTenthLeft.Click

        Dim b() As Byte = New Byte() {114}

        distance_travelled -= 0.1
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnHunLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHunLeft.Click

        Dim b() As Byte = New Byte() {115}

        distance_travelled -= 0.01
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnMilLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMilLeft.Click

        Dim b() As Byte = New Byte() {116}

        distance_travelled -= 0.001
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnInchRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInchRight.Click
        ' move one inch button
        Dim b() As Byte = New Byte() {109}

        ' update the distance travelled textbox
        distance_travelled += 1.0
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnTenthRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTenthRight.Click

        Dim b() As Byte = New Byte() {110}

        distance_travelled += 0.1
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnHunRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHunRight.Click

        Dim b() As Byte = New Byte() {111}

        distance_travelled += 0.01
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnMilRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMilRight.Click

        Dim b() As Byte = New Byte() {112}

        distance_travelled += 0.001
        txtDistTravel.Text = distance_travelled

        btnDisableMotor.ForeColor = Color.Black

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    ' check the flags
    Private Sub checkWhatsBeenSent()
        Dim cut_length As Decimal
        If are_traces_sent And is_quantity_sent Then
            ' get cut length, in inches
            cut_length = (g_traces + 1) / 2
            cut_length = cut_length / 25.4
            cut_length = Decimal.Round(cut_length, 3)
            txtCutLength.Text = cut_length
            lblCutLength.Visible = True
            txtCutLength.Visible = True
            lblCutLengthInches.Visible = True
        End If
    End Sub
End Class
