﻿Public Class frmSpeechConsole
    Dim oloc As Point
    Private Sub fbHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fbHide.Click
        Me.Hide()
    End Sub

    Private Sub fbOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fbOnOff.Click
        modSpeech.SpeechEnabled = Not modSpeech.SpeechEnabled
    End Sub

    Private Sub txtEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEntry.KeyDown
        If e.KeyCode = Keys.Enter Then
            SimulateRecognition(txtEntry.Text)
            txtEntry.Clear()
        End If
    End Sub

    Private Sub elbTop_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles elbTop.MouseDown
        oloc = New Point(MousePosition.X, MousePosition.Y)
    End Sub

    Private Sub elbTop_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles elbTop.MouseMove
        If MouseButtons = Windows.Forms.MouseButtons.Left Then
            Me.Left += MousePosition.X - oloc.X
            Me.Top += MousePosition.Y - oloc.Y
            oloc = New Point(MousePosition.X, MousePosition.Y)
            Application.DoEvents()
        End If
    End Sub

    Private Sub frmSpeechConsole_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Hide()
        e.Cancel = True
    End Sub

    Public Sub AddCommand(ByVal command As VoiceCmd)
        Me.lstCommands.Items.Add(command)
    End Sub

    Public Sub ClearCommands()
        Me.lstCommands.Items.Clear()
    End Sub

    Public Sub WriteLine(ByVal line As String)
        line = line & vbNewLine
        If txtHistory.Text.Length() + line.Length > txtHistory.MaxLength Then
            'Erase enough lines to fit
            Dim loc As Integer = 0
            While txtHistory.Text.Length + line.Length - loc > txtHistory.MaxLength
                loc = txtHistory.Text.IndexOf(vbNewLine, loc) + vbNewLine.Length
                If loc = -1 Then
                    loc = txtHistory.Text.Length - vbNewLine.Length
                    Exit While
                End If
            End While
            txtHistory.Text = txtHistory.Text.Substring(loc)
        End If
        txtHistory.AppendText(line)
        Application.DoEvents()
    End Sub

    Private Sub frmSpeechConsole_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler modSpeech.SpeechEnableChanged, AddressOf Speech_enableChanged
    End Sub

    Private Sub Speech_enableChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If modSpeech.SpeechEnabled Then
            console.fbOnOff.Lit = True
            console.fbOnOff.Text = "Recognition on"
        Else
            console.fbOnOff.Lit = False
            console.fbOnOff.Text = "Recognition off"
        End If
    End Sub

    Private Sub fbClearHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fbClearHistory.Click
        txtHistory.Clear()
    End Sub
End Class