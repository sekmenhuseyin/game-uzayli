Imports System.Runtime.InteropServices 'eklemeyi unutmayýn
Imports System.Threading 'eklemeyi unutmayýn

Public Class Form1

    'basic te apinin tanýmlanmasý
    Declare Function PlaySound Lib "winmm.dll" Alias "PlaySoundA" _
  (ByVal Yol As String, ByVal Numara As Long, ByVal Ozellik As Long) As Long

#Region "global deðiþkenler"

    Dim kanal As Thread
    Dim kanal_vurulunca As Thread

    'canavarlarýn oluþturulmasý için gerekli label matrisini tanýmladým
    Dim dizi_olustur(,) As Label
    Dim elemaný_kaydýr As Integer = 5

    'canavarlarýn ateþi için 10 elemanlý birdizi oluþtur
    Dim ates(9) As PictureBox
    'ates etmek için 3 elemanlý bir dizi oluþtur
    Dim ates2(2) As PictureBox

    'ateþ eden elemanýmýzý koruyan siperleri
    Dim dizi_siper1(2, 29) As Label
    Dim dizi_siper2(2, 29) As Label
    Dim aralýk_birak As Integer = -10

    'canavarlardan her biri vurulduðunda puaný arttýrýcaz
    Dim puan As Integer = 0

    'puan label ýný oluþtur
    Dim puani_yaz As New Label()

    'ateþ eden elemanýn vurulma sayýsýný tut
    Dim sayac_vurulma As Integer = 0

    'ateþ eden elemanýn hareket artýmý
    Dim h As Integer = 1

    Dim elemanlari_asagi_indir As Integer = 5

#End Region

#Region "prosedürler"

    Private Sub oyunu_baslat()

        label1.Visible = False
        elemanlarý_olustur()
        timer1.Enabled = True

        'zorluk derecesini menu itemdan gelen 
        'cevaba göre ayarla
        If (kolayToolStripMenuItem.Checked) Then
            timer1.Interval = 250
        ElseIf (ortaToolStripMenuItem.Checked) Then
            timer1.Interval = 150
        ElseIf (zorToolStripMenuItem.Checked) Then
            timer1.Interval = 100
        Else
            timer1.Interval = 250
        End If

        Me.KeyPreview = True
        timer2.Interval = 1
        timer3.Interval = 1
        timer4.Interval = 1

        'menu strip in visible ýný false yap
        menuStrip1.Enabled = False

        'hak sayýsýný formun baþlýðýna yaz
        Me.Text = "KALAN HAK SAYISI :  " + "5"

        'ates eden elemanýn görünürlüðünü true yap
        picturebox_ates.Visible = True


        'puan ve hak deðiþkenlerini sýfýrla
        puan = 0
        sayac_vurulma = 0
        puani_yaz.ResetText()
    End Sub

    Private Sub oyunu_bitir()
        'önce timerlarý durdur..
        timer1.Enabled = False
        timer2.Enabled = False
        timer3.Enabled = False
        timer4.Enabled = False
        'aldýðý puaný ve oyuncu ismini göster
        MessageBox.Show(toolStripTextBox1.Text + "       PUAN: " + puan.ToString(), "OYUN BÝTTÝ", MessageBoxButtons.OK, MessageBoxIcon.Information)

        'enabled ý false olan menustrip i true yap
        menuStrip1.Enabled = True
        Dim i, j As Integer
        'oluþturulmuþ elemanlarý yok et
        For i = 0 To 9
            'canavarlarý yok et
            For j = 0 To 4
                dizi_olustur(i, j).Dispose()
            Next
        Next

        For i = 0 To 2
            'siperleri yok et
            For j = 0 To 29
                dizi_siper1(i, j).Dispose()
                dizi_siper2(i, j).Dispose()
            Next
        Next

        For i = 0 To 2
            ates2(i).Dispose()
            Me.Refresh()
        Next
        kanal.Abort()
        kanal_vurulunca.Abort()
    End Sub

    Private Sub elemanlarý_olustur()
        'yukarýdan aþaðý inen canavarlarý  oluþtur
        Dim x_kordinatý As Integer = 10
        Dim y_kordinatý As Integer = 20
        ReDim dizi_olustur(9, 4)

        Dim i, j As Integer
        For i = 0 To 9
            For j = 0 To 4
                Dim olustur As New Label()
                dizi_olustur(i, j) = olustur
                Me.Controls.Add(dizi_olustur(i, j))
                dizi_olustur(i, j).SetBounds(x_kordinatý, y_kordinatý, 35, 40)
                dizi_olustur(i, j).Image = label1.Image
                x_kordinatý += 25
                If (x_kordinatý = 260) Then
                    x_kordinatý = 10
                    y_kordinatý += 35
                End If

            Next
        Next
        '**********************************************'
        'SÝPERLERÝ OLUÞTUR
        Dim x_kor1 As Integer = 50
        Dim y_kor1 As Integer = 375
        Dim x_kor2 As Integer = 350
        Dim y_kor2 As Integer = 375

        For i = 0 To 2
            For j = 0 To 29
                Dim olustur1 As New Label()
                Dim olustur2 As New Label()
                dizi_siper1(i, j) = olustur1
                dizi_siper2(i, j) = olustur2
                dizi_siper1(i, j).BackColor = Color.Navy
                dizi_siper2(i, j).BackColor = Color.Navy
                Me.Controls.Add(dizi_siper1(i, j))
                Me.Controls.Add(dizi_siper2(i, j))
                dizi_siper1(i, j).SetBounds(x_kor1, y_kor1, 4, 10)
                dizi_siper2(i, j).SetBounds(x_kor2, y_kor2, 4, 10)
                x_kor1 += 4

                If (x_kor1 = 170) Then
                    x_kor1 = 50
                    y_kor1 += 10
                End If
                x_kor2 += 4
                If (x_kor2 = 470) Then
                    x_kor2 = 350
                    y_kor2 += 10
                End If
            Next
        Next
    End Sub

    Private Sub elemanlarýn_hareketi()
        Dim i, j, k, l As Integer

        For i = 0 To 9
            For j = 0 To 4
                'elemanlar sag veya sol uca geldiklerinde aþaðý inerek geri dönsünler
                If (dizi_olustur(i, j).Visible = False) Then

                    'eleman vurulmuþsa onu  geç devam et
                    'vurulmamýþ olanlardan saða yada sola gelen var mý bak
                    Continue For
                End If
                'eðer varsa ozaman elemanlarý kaydýr
                If ((dizi_olustur(i, j).Left >= Me.Width - 30) Or (dizi_olustur(i, j).Left <= 0)) Then

                    For k = 0 To 9
                        For l = 0 To 4
                            If (dizi_olustur(i, j).Visible = False) Then
                                'eleman vurulmuþsa onu  geç devam et
                                'vurulmamýþ olanlarý aþaðý indir
                                Continue For
                            End If
                            dizi_olustur(k, l).Top += elemanlari_asagi_indir
                        Next
                    Next
                    For k = 0 To 9
                        For l = 0 To 4
                            If (dizi_olustur(i, j).Visible = False) Then
                                'eleman vurulmuþsa onu  geç devam et
                                'vurulmamýþ olanlarý aþaðý indir
                                Continue For
                            End If
                            If dizi_olustur(k, l).Top >= 400 Then
                                oyunu_bitir()
                                Exit For
                            End If
                        Next
                    Next
                    'aþaðý indirdikten sonra geri dönmelerini istediðimiz
                    'için hareket yönünü ters çevir
                    elemaný_kaydýr = -elemaný_kaydýr
                    Return 'iþlem yapýlýnca çýk devam etme 
                End If
            Next
        Next
    End Sub

    Private Sub elemanlarýn_atesi()

        If (timer2.Enabled = True) Then
            'atýlan atesler hala gidiyorsa yeni üretme çýk
            Return
        End If

        'vurulmamýþ canavarlar arasýndan ateþ edecekleri seç
        Dim i As Integer
        For i = 0 To 9
            Dim k As Integer = Rnd() * 9
            Dim l As Integer = Rnd() * 4

            'random seçilen canavarýn visible ý true ise 
            'yani vurulmamýþsa ateþ etsin
            If (dizi_olustur(k, l).Visible = True) Then
                Dim ates_et As New PictureBox()
                ates(i) = ates_et
                Me.Controls.Add(ates(i))
                ates(i).BackColor = Color.Red
                ates(i).SetBounds(dizi_olustur(k, l).Location.X + 20, dizi_olustur(k, l).Location.Y + 20, 2, 4)
                timer2.Interval = 1
                timer2.Enabled = True
            End If

        Next

    End Sub

    Private Sub siperleri_vur()

        timer2.Enabled = False
        ' canavarlarýn attýðý ateþler ile siperlerin koordinatlarýna bak
        'ayný koordinatta olan siperleri sil
        Dim z, i, j As Integer
        For z = 0 To 9
            For i = 0 To 2
                For j = 0 To 29

                    If ((ates(z).Left >= dizi_siper1(i, j).Left) And (ates(z).Left <= dizi_siper1(i, j).Left + dizi_siper1(i, j).Width)) Then

                        If ((ates(z).Top >= dizi_siper1(i, j).Top) And (ates(z).Top <= dizi_siper1(i, j).Top + dizi_siper1(i, j).Height)) Then

                            dizi_siper1(i, j).Dispose()
                            dizi_siper1(i, j).Left = 1000
                            'ates i yok ediyorum
                            ates(z).Left = 1000
                        End If
                    End If
                    If ((ates(z).Left >= dizi_siper2(i, j).Left) And (ates(z).Left <= dizi_siper2(i, j).Left + dizi_siper2(i, j).Width)) Then

                        If ((ates(z).Top >= dizi_siper2(i, j).Top) And (ates(z).Top <= dizi_siper2(i, j).Top + dizi_siper2(i, j).Height)) Then

                            dizi_siper2(i, j).Dispose()
                            dizi_siper2(i, j).Left = 1000
                            'ates i yok ediyorum
                            ates(z).Left = 1000
                        End If
                    End If

                Next
                'ates elemanýný vurmussa sayacý arttýr
                If ((ates(z).Left >= picturebox_ates.Left) And (ates(z).Left <= picturebox_ates.Left + picturebox_ates.Width)) Then

                    If ((ates(z).Top >= picturebox_ates.Top) And (ates(z).Top <= picturebox_ates.Top + picturebox_ates.Height)) Then

                        kanal_vurulunca = New Thread(AddressOf vurulunca_ses)
                        kanal_vurulunca.Start()

                        sayac_vurulma += 1
                        Me.Text = "KALAN HAK SAYISI :  " + (5 - sayac_vurulma).ToString()
                        'ateþi yok ediyorum
                        ates(z).Left = 1000
                        If (5 - sayac_vurulma = 0) Then

                            'oyunu bitirme prosedürünü çalýþtýr...
                            oyunu_bitir()
                        End If
                    End If

                End If
            Next
        Next
        timer2.Enabled = True
    End Sub

    Private Sub elemanlari_vur()

        timer4.Enabled = False
        'ateþ eden elemanýmýzýn attýðý ateþler eðer canavarlarýn
        'bulunduðu koordinattan geçerse elemanlarý yok et
        Dim z, i, j As Integer
        For z = 0 To 2
            For i = 0 To 9
                For j = 0 To 4
                    If ((ates2(z).Left >= dizi_olustur(i, j).Left) And (ates2(z).Left <= dizi_olustur(i, j).Left + dizi_olustur(i, j).Width)) Then

                        If ((ates2(z).Top >= dizi_olustur(i, j).Top) And (ates2(z).Top <= dizi_olustur(i, j).Top + dizi_olustur(i, j).Height)) Then

                            dizi_olustur(i, j).Visible = False
                            dizi_olustur(i, j).Left = 1000
                            'ates i yok ediyorum
                            ates2(z).Left = 1000
                            ates2(z).Top = -1000
                            'her vurulan canavardan sonra puaný 50 arttýr
                            'menüden alýnan ismide ekle
                            puan += 50
                            puani_yaz.Text = toolStripTextBox1.Text + "       PUAN: " + puan.ToString()
                            If (puan = 2500) Then

                                oyunu_bitir()
                            End If
                        End If
                    End If
                Next
            Next
        Next
        timer4.Enabled = True
    End Sub

    Private Sub ates_ses()
        'SES ÝÇÝN APÝ KULLANDIM
        Dim Numara As Long
        Dim yol As String = System.IO.Directory.GetCurrentDirectory
        Dim Dosya As String = yol + "\ates.WAV"
        Numara = PlaySound(Dosya, 0, 1)
        kanal.Abort()
    End Sub

    Private Sub vurulunca_ses()
        Dim Numara As Long
        Dim yol As String = System.IO.Directory.GetCurrentDirectory
        Dim Dosya As String = yol + "\vurulunca.WAV"
        Numara = PlaySound(Dosya, 0, 1)
        kanal_vurulunca.Abort()
    End Sub

    Private Sub oyucunun_atesini_olustur()

        If (timer4.Enabled = True) Then
            'atýlan atesler hala gidiyorsa yeni üretme çýk
            Return
        End If

        kanal = New Thread(AddressOf ates_ses)
        kanal.Start()

        Dim i As Integer
        For i = 0 To 2
            '3 tane ateþ üret
            Dim ates_et As New PictureBox()
            ates2(i) = ates_et
            Me.Controls.Add(ates2(i))
            'oluþturulan ateþler aralarýnda biraz aralýkla oluþturulsun
            ates2(i).SetBounds(picturebox_ates.Location.X + (picturebox_ates.Width / 2) + aralýk_birak, picturebox_ates.Location.Y + 2 * aralýk_birak, 3, 5)
            aralýk_birak += 10
            ates2(i).BackColor = Color.Blue
            timer4.Enabled = True
        Next
        aralýk_birak = -10

    End Sub

#End Region

#Region "eventler"
    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        timer3.Enabled = True
        'basýlan yön tuþu hangisiyse ona göre 
        'gitmesi gereken timer ile çalýþtýr
        If ((e.KeyCode = Keys.Left) And (h > 0)) Then
            h = h * -1
        ElseIf ((e.KeyCode = Keys.Right) And (h < 0)) Then
            h *= -1
        ElseIf (e.KeyCode = Keys.Space) Then
            oyucunun_atesini_olustur()
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ilk ayarlamalar yapýlýrken iki nesnenin de görselliðini kapat
        label1.Visible = False
        picturebox_ates.Visible = False

        'formun boyutunu ayarla
        Me.FormBorderStyle = FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterScreen

        'formun yüksekliðini
        Me.Height = Me.Height + picturebox_ates.Height + puani_yaz.Height + 15

        'menustrip in fontunu deðiþtir
        menuStrip1.Font = New Font("Comic Sans MS", 9, FontStyle.Bold)

        'label ýn yazým stilini deðiþtir
        puani_yaz.Font = New Font("Comic Sans MS", 10, FontStyle.Bold)


        'pictureboxýn resim modunu deðiþtirdim
        picturebox_ates.SizeMode = PictureBoxSizeMode.StretchImage

        'puaný yazacaðýmýz label ý forma ekle
        Me.Controls.Add(puani_yaz)
        puani_yaz.SetBounds(0, picturebox_ates.Top + picturebox_ates.Height + 30, 500, 20)

        'resmin arka rengini formun backcolor ý yap
        Me.BackColor = Color.FromArgb(128, 128, 255)

        'formun size ýný ayarla
        Me.Width = 560
        Me.Height = 540

        'ateþ elemanýný ayarla
        picturebox_ates.Left = 230
        picturebox_ates.Top = 435
    End Sub

    Private Sub timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer1.Tick
        'canavarlarý saða ya da sola kaydýrýyor
        Dim i, j As Integer
        For i = 0 To 9
            For j = 0 To 4
                If (dizi_olustur(i, j).Visible = False) Then
                    Continue For
                End If
                dizi_olustur(i, j).Left += elemaný_kaydýr
            Next
        Next
        'elemanlar sag veya sol uca geldi mi bak...
        elemanlarýn_hareketi()
        'ateþ ettir..
        elemanlarýn_atesi()
    End Sub

    Private Sub timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer2.Tick
        'canavarlarýn ateþi için
        siperleri_vur()
        Dim i As Integer
        If (ates(9).Top >= Me.Height + 50) Then
            'eðer son ates elemaný formun yüksekliðinden 50 fazlaya gelince 
            'ates leri yok et tekrar yapýlmasý için hazýrla
            For i = 0 To 9
                ates(i).Dispose()
            Next
            timer2.Enabled = False
        End If
        For i = 0 To 9
            ates(i).Top += 2
        Next
    End Sub

    Private Sub timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer3.Tick
        'ateþ eden elemanýmýz formun en soluna ya da en saðýna gelmiþse daha fazla gitmesini engelle
        If (picturebox_ates.Left < 0) Then
            h *= -1
        End If
        If (picturebox_ates.Left > Me.Width - picturebox_ates.Width) Then
            h *= -1
        End If
        picturebox_ates.Left += h
    End Sub

    Private Sub timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer4.Tick
        'ateþ eden elemanýmýzýn ateþi için
        elemanlari_vur()
        Dim i As Integer
        If (ates2(2).Top < 0) Then
            'eðer son ates elemaný formun yüksekliðinden 50 fazlaya lince 
            'ates leri yok et tekrar yapýlmasý için hazýrla
            For i = 0 To 2
                ates2(i).Dispose()
            Next
            timer4.Enabled = False
        End If
        For i = 0 To 2
            ates2(i).Top -= 1
        Next
    End Sub

    Private Sub yeniOyunBaþlatToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles yeniOyunBaþlatToolStripMenuItem1.Click
        'oyunu baþlatmak için gerekli timerlarýn açýlmasýný saðlayan
        'prosedürü çalýþtýrýyorum
        oyunu_baslat()
    End Sub

    Private Sub kolayToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles kolayToolStripMenuItem.Click, ortaToolStripMenuItem.Click, zorToolStripMenuItem.Click
        'kolay,orta ve zortoolstripmenuitem lar buraya baðlandý
        If (sender Is kolayToolStripMenuItem) Then
            kolayToolStripMenuItem.Checked = True
            ortaToolStripMenuItem.Checked = False
            zorToolStripMenuItem.Checked = False
            elemanlari_asagi_indir = 5
        ElseIf (sender Is ortaToolStripMenuItem) Then
            ortaToolStripMenuItem.Checked = True
            kolayToolStripMenuItem.Checked = False
            zorToolStripMenuItem.Checked = False
            elemanlari_asagi_indir = 10
        ElseIf (sender Is zorToolStripMenuItem) Then
            zorToolStripMenuItem.Checked = True
            kolayToolStripMenuItem.Checked = False
            ortaToolStripMenuItem.Checked = False
            elemanlari_asagi_indir = 20
        End If
    End Sub
#End Region




End Class
