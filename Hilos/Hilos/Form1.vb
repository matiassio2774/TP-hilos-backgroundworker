Imports System.Threading

Imports System.IO
Imports System.Text

Imports System.IO.StreamWriter

Public Class Form1
    Inherits System.Windows.Forms.Form
    <STAThread()>
    Public Shared Sub Main()
        Dim hilo1 As New Thread(AddressOf Primero)
        Dim hilo2 As New Thread(AddressOf Segundo)
        hilo1.Start()
        hilo2.Start()
    End Sub

    Public Shared Sub Primero()
        Dim Texto As String = Form1.TextBox2.Text
        Segundo(Texto)
    End Sub
    Public Shared Sub Segundo(Aux As String)
        Dim Max As Integer = Form1.TextBox1.Text
        Form1.ProgressBar1.Maximum = Max

        Dim Palabras As String() = Form1.TextBox2.Text.Split(" ")
        Dim Palabra As String = ""
        Dim Contador As Integer = 0
        Dim Linea As String = ""
        Contador = 0

        Dim path As String = "C:\Users\PC1\Desktop\Hilos.txt"
        Dim fs As FileStream = File.Create(path)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(Aux)
        fs.Write(info, 0, info.Length)

        For Each Palabra In Palabras
            Linea = Linea & Palabra & " "
            Contador += 1
        Next

        Dim lineas As String() = Form1.TextBox2.Lines()
        Contador -= 1
        For Each lin As String In lineas
            Dim values As Object() = {lin}
            Contador += 1
        Next

        Form1.ProgressBar1.Value = Contador
        Form1.Label2.Text = (Contador / Max) * 100 & "%"
        Form1.Label2.Refresh()
        fs.Close()
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
        Else
            MsgBox("Inserte cantidad maxima de lineas de texto")
        End If
    End Sub
End Class
