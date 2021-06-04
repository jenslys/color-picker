Public Class Form1
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Start()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Stop()
        Dim item As New ListViewItem(TextBox2.Text)
        item.SubItems.Add(TextBox2.Text)
        ListView1.Items.Add(item)
    End Sub

    'Get current color
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim BMP As New Bitmap(1, 1)
        Dim GFX As Graphics = Graphics.FromImage(BMP)
        GFX.CopyFromScreen(New Point(MousePosition.X, MousePosition.Y),
        New Point(0, 0), BMP.Size)
        Dim Pixel As Color = BMP.GetPixel(0, 0)
        Panel1.BackColor = Pixel
        TextBox1.Text = Pixel.R & "," & Pixel.G & "," & Pixel.B
        Dim R As Integer = Integer.Parse(Pixel.R)
        Dim G As Integer = Integer.Parse(Pixel.G)
        Dim B As Integer = Integer.Parse(Pixel.B)
        Dim hexValue As String = String.Format("{0}{1}{2}", R.ToString("X").PadLeft(2, "0"), G.ToString("X").PadLeft(2, "0"), B.ToString("X").PadLeft(2, "0"))
        TextBox2.Text = "#" & hexValue
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        TextBox1.SelectAll()
    End Sub

    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        TextBox2.SelectAll()
    End Sub

    'Stop hotkey
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles HotkeyStart.Tick
        If (GetAsyncKeyState(120)) Then
            HotkeyStart.Stop()
            Button2.PerformClick()
            Threading.Thread.Sleep(200)
            HotkeyStart.Start()
        End If
    End Sub

    'Start hotkey
    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles HotkeyStop.Tick
        If (GetAsyncKeyState(119)) Then
            HotkeyStart.Stop()
            Button1.PerformClick()
            Threading.Thread.Sleep(200)
            HotkeyStart.Start()
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(ListView1.SelectedItems(0).SubItems(0).Text)
    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click
        ListView1.Clear()
    End Sub

    ' Clicking on a color for the history list
    Private Sub ListView1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        If ListView1.SelectedItems.Count = 0 Then Return
        Panel1.BackColor = ColorTranslator.FromHtml(ListView1.SelectedItems(0).SubItems(0).Text)
        TextBox2.Text = ListView1.SelectedItems(0).SubItems(0).Text
        TextBox1.Text = Nothing 'Temporary - will fix it
    End Sub

End Class