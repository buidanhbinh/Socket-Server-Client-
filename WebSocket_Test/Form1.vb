Imports System.Net
Imports System.Net.Sockets
Imports System.Net.WebSockets
Imports System.Text
Imports System.Threading
Public Class Form1
    Public port As String = "8080"
    Public ip As String = "127.0.0.1"
    Public g As Socket
    Public j As New List(Of Socket)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        g = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        g.Bind(New IPEndPoint(IPAddress.Parse(ip), port))
        g.Listen(1)
        Dim Context As TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()
        Task.Factory.StartNew(Sub() AcceptAndRead(Context), CancellationToken.None, TaskCreationOptions.LongRunning)
    End Sub
    Public Sub AcceptAndRead(ByVal Context As TaskScheduler)
        Try
            While (True)
                Dim p As Socket = g.Accept
                j.Add(p)
                ListView1.Items.Add(p.RemoteEndPoint.ToString)
                Task.Run(Sub() RD(p, Context, p.RemoteEndPoint.ToString))
            End While
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    ''  Public iop As StringBuilder
    Private Sub RD(ByVal stream As Socket, ByVal context As TaskScheduler, ByVal id As String)
        ''
        ''   iop = New StringBuilder
        While True
            Dim Buffer(4096) As Byte
            stream.Receive(Buffer)
            Try
                Dim Message As String = Encoding.UTF8.GetString(Buffer)
                MessageBox.Show(Message)
            Catch ex As Exception
            End Try
        End While
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For Each k As Socket In j
            Dim o As Byte() = System.Text.Encoding.UTF8.GetBytes("Hello from server !")
            k.Send(o)
        Next
    End Sub
End Class
