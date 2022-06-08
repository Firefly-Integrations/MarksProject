Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim filereader As String
        filereader = My.Computer.FileSystem.ReadAllText("C:\Users\ethana\stuff2.txt")
        MsgBox(filereader)
    End Sub
End Class
