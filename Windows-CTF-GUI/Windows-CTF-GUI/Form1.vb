Imports System.IO
Imports System.IO.Ports
Imports System.Threading

Public Class Form1

    Shared _continue As Boolean
    Shared SerialPort1 = New SerialPort()

    Private Shared Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SerialPort1.PortName = "COM6" ' check And change Arduino port
        SerialPort1.BaudRate = 9600
        SerialPort1.DataBits = 8
        SerialPort1.Parity = Parity.None
        SerialPort1.StopBits = StopBits.One
        SerialPort1.Handshake = Handshake.None
        SerialPort1.Encoding = System.Text.Encoding.Default

    End Sub
    Private Sub btnHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHome.Click

        Dim b() As Byte = New Byte() {100}

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnMoveCW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveCW.Click

        Dim b() As Byte = New Byte() {101}

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnCutElements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCutElements.Click

        Dim b() As Byte = New Byte() {102}

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnDisableMotor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisableMotor.Click

        Dim b() As Byte = New Byte() {103}

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnBladeDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBladeDown.Click

        Dim b() As Byte = New Byte() {107}

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnMoveCCW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveCCW.Click

        Dim b() As Byte = New Byte() {106}

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub
    Private Sub btnSendTraces_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendTraces.Click
        Dim value As Integer
        value = Convert.ToInt32(txtTraces.Text)
        Dim b() As Byte = New Byte() {value}

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub

    Private Sub btnSendQuantity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendQuantity.Click

        Dim value As Integer
        value = Convert.ToInt32(txtQuantity.Text)
        value += 200
        Dim b() As Byte = New Byte() {value}

        SerialPort1.Open()
        SerialPort1.Write(b, 0, 1)
        SerialPort1.Close()

    End Sub

End Class