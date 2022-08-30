Imports System.Threading

Imports System.IO
Imports System.Text

Imports System.IO.StreamWriter

Public Class Form1
    Inherits System.Windows.Forms.Form
    <STAThread()>
    Public Shared Sub Main()
        Dim hilo1 As New Thread(AddressOf Primero)
        hilo1.Start()
    End Sub

    Public Shared Sub Primero()
        Dim Texto As String = Form1.TextBox2.Text
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Dim i As Integer


        For i = 1 To 1000
            BackgroundWorker1.WorkerReportsProgress = True
            BackgroundWorker1.ReportProgress(i / 10)
        Next


    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged

        If (e.ProgressPercentage <= Val(TextBox1.Text)) Then
            ProgressBar1.Value = e.ProgressPercentage
        End If


    End Sub



    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        MsgBox("listo")

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Primero()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Val(TextBox1.Text) > 0 Then
            Button2.Visible = False
            TextBox1.Enabled = False
            BackgroundWorker1.RunWorkerAsync()

            Dim maximo As Integer = Val(TextBox1.Text)
            ProgressBar1.Maximum = maximo

            Dim path As String = "C:\Users\PC1\Desktop\Hilos.txt"
            Dim fs As FileStream = File.Create(path)

            For i = 1 To maximo Step 1
                Dim info As Byte() = New UTF8Encoding(True).GetBytes("Linea " + Str(i) + vbCrLf)
                fs.Write(info, 0, info.Length)
            Next

            fs.Close()

        Else
            MsgBox("Inserte cantidad maxima de lineas de texto")
        End If
    End Sub
End Class
