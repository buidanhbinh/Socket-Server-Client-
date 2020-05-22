Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Public Class Form1
    Public port As String = "8080"
    Public ip As String = "127.0.0.1"
    Public o As Socket
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        o = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Dim MonEP As IPEndPoint = New IPEndPoint(IPAddress.Parse(ip), port)
        o.Connect(MonEP)
        Dim k As New Thread(Sub() ReadMSG(o))
        k.Start()
    End Sub
    Private Sub ReadMSG(ByVal SocketReception As Socket)
        While True
            Try
                Dim l(4096) As Byte
                SocketReception.Receive(l)
                MessageBox.Show(System.Text.Encoding.UTF8.GetString(l))
            Catch ex As Exception
            End Try
        End While
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim oaz As Byte() = System.Text.Encoding.UTF8.GetBytes("Hello from Client !")
        o.Send(oaz)
    End Sub
End Class
