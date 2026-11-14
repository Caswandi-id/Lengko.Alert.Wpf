Imports System.Windows
Imports System.Windows.Media
Imports Lengko.Alert.Wpf

Class MainWindow
    Private _clickCount As Integer = 0

    Private Sub BtnCounter_Click(sender As Object, e As RoutedEventArgs) Handles BtnCounter.Click
        _clickCount += 1
        BtnCounter.Content = String.Format("Test UI Responsiveness (Click Count: {0})", _clickCount)
    End Sub

    ' 1. Basic Modal Alerts
    Private Sub BtnSuccess_Click(sender As Object, e As RoutedEventArgs) Handles BtnSuccess.Click
        LengkoAlert.Fire("Berhasil!", "Data tersimpan dengan sukses ke database.", AlertIcon.Success)
    End Sub

    Private Sub BtnError_Click(sender As Object, e As RoutedEventArgs) Handles BtnError.Click
        LengkoAlert.Fire("Oops...", "Terjadi kesalahan saat memproses data.", AlertIcon.Error)
    End Sub

    Private Sub BtnWarning_Click(sender As Object, e As RoutedEventArgs) Handles BtnWarning.Click
        LengkoAlert.Fire("Peringatan!", "Sesi login Anda akan segera berakhir dalam 5 menit.", AlertIcon.Warning)
    End Sub

    Private Sub BtnInfo_Click(sender As Object, e As RoutedEventArgs) Handles BtnInfo.Click
        LengkoAlert.Fire("Informasi", "Lengko.Alert.Wpf berjalan murni tanpa dependensi eksternal.", AlertIcon.Info)
    End Sub

    Private Sub BtnQuestion_Click(sender As Object, e As RoutedEventArgs) Handles BtnQuestion.Click
        LengkoAlert.Fire("Pertanyaan", "Apakah Anda ingin melanjutkan sinkronisasi data sekarang?", AlertIcon.Question)
    End Sub

    ' 2. Confirm & Prompt
    Private Sub BtnConfirmSync_Click(sender As Object, e As RoutedEventArgs) Handles BtnConfirmSync.Click
        Dim ok As Boolean = LengkoAlert.Confirm(
            "Konfirmasi Hapus",
            "Yakin ingin menghapus record ini secara permanen?",
            confirmText:="Ya, Hapus!",
            cancelText:="Batal",
            icon:=AlertIcon.Warning)

        If ok Then
            LengkoAlert.Fire("Terhapus!", "Record berhasil dihapus dari sistem.", AlertIcon.Success)
        End If
    End Sub

    Private Async Sub BtnConfirmAsync_Click(sender As Object, e As RoutedEventArgs) Handles BtnConfirmAsync.Click
        Dim ok As Boolean = Await LengkoAlert.ConfirmAsync(
            "Konfirmasi Logout",
            "Apakah Anda yakin ingin keluar dari aplikasi?",
            confirmText:="Logout",
            cancelText:="Batal",
            icon:=AlertIcon.Question)

        If ok Then
            LengkoAlert.ShowToast("Anda telah logout.", AlertIcon.Info)
        End If
    End Sub

    Private Sub BtnPromptSync_Click(sender As Object, e As RoutedEventArgs) Handles BtnPromptSync.Click
        Dim name As String = LengkoAlert.Prompt("Input Nama Lengkap", placeholder:="Nama Anda...", defaultValue:="Caswandi")
        If Not String.IsNullOrEmpty(name) Then
            LengkoAlert.Fire("Halo!", String.Format("Selamat datang, {0}!", name), AlertIcon.Success)
        End If
    End Sub

    Private Async Sub BtnPromptAsync_Click(sender As Object, e As RoutedEventArgs) Handles BtnPromptAsync.Click
        Dim email As String = Await LengkoAlert.PromptAsync("Input Email", placeholder:="email@contoh.com")
        If Not String.IsNullOrEmpty(email) Then
            LengkoAlert.ShowToast(String.Format("Email '{0}' tersimpan!", email), AlertIcon.Success)
        End If
    End Sub

    Private Sub BtnPromptValidator_Click(sender As Object, e As RoutedEventArgs) Handles BtnPromptValidator.Click
        Dim pin As String = LengkoAlert.Prompt(
            "Masukkan PIN Keamanan",
            placeholder:="Minimal 6 digit...",
            validator:=Function(val As String) As String
                           If String.IsNullOrWhiteSpace(val) Then Return "PIN tidak boleh kosong!"
                           If val.Length < 6 Then Return "PIN minimal harus 6 karakter!"
                           Return Nothing
                       End Function)

        If pin IsNot Nothing Then
            LengkoAlert.Fire("Akses Diberikan", "PIN valid!", AlertIcon.Success)
        End If
    End Sub

    ' 3. Corner Toasts
    Private Sub BtnToastSuccess_Click(sender As Object, e As RoutedEventArgs) Handles BtnToastSuccess.Click
        LengkoAlert.ShowToast("Sinkronisasi Selesai!", AlertIcon.Success)
    End Sub

    Private Sub BtnToastWarning_Click(sender As Object, e As RoutedEventArgs) Handles BtnToastWarning.Click
        LengkoAlert.ShowToast("Koneksi Tidak Stabil", "Mencoba menghubungkan kembali...", AlertPosition.TopStart, AlertIcon.Warning)
    End Sub

    Private Sub BtnToastError_Click(sender As Object, e As RoutedEventArgs) Handles BtnToastError.Click
        LengkoAlert.ShowToast("Gagal Mengunduh File", "Server timeout (504)", AlertPosition.BottomEnd, AlertIcon.Error)
    End Sub

    Private Sub BtnToastInfo_Click(sender As Object, e As RoutedEventArgs) Handles BtnToastInfo.Click
        LengkoAlert.ShowToast("Pembaruan Tersedia", "Versi 1.0.0 siap diunduh.", AlertPosition.Bottom, AlertIcon.Info)
    End Sub

    Private Sub BtnToastCustom_Click(sender As Object, e As RoutedEventArgs) Handles BtnToastCustom.Click
        LengkoAlert.ShowToast(New AlertOptions With {
            .Title = "Notifikasi Custom",
            .Text = "Toast aktif selama 5 detik dengan animasi SlideSide.",
            .Position = AlertPosition.TopEnd,
            .Icon = AlertIcon.Success,
            .Timer = 5000,
            .TimerProgressBar = True,
            .OpenAnimation = AlertAnimation.SlideSide
        })
    End Sub

    ' 4. Advanced & Custom Options
    Private Sub BtnDarkTheme_Click(sender As Object, e As RoutedEventArgs) Handles BtnDarkTheme.Click
        LengkoAlert.Fire(New AlertOptions With {
            .Title = "Dark Theme Modal",
            .Text = "Tampilan elegan tema gelap slate dengan kontras tinggi.",
            .Icon = AlertIcon.Info,
            .Theme = AlertTheme.Dark,
            .ShowCancelButton = True,
            .CancelButtonText = "Tutup",
            .ConfirmButtonText = "Keren!"
        })
    End Sub

    Private Sub BtnNonModal_Click(sender As Object, e As RoutedEventArgs) Handles BtnNonModal.Click
        LengkoAlert.Fire(New AlertOptions With {
            .Title = "Non-Modal Floating Alert",
            .Text = "Alert ini mengambang di layar tanpa memblokir interaksi window utama! Coba klik tombol Counter di bawah.",
            .Icon = AlertIcon.Info,
            .Modal = False,
            .Backdrop = False,
            .Position = AlertPosition.CenterEnd,
            .ShowConfirmButton = True,
            .ConfirmButtonText = "Mengerti"
        })
    End Sub

    Private Sub BtnPillButtons_Click(sender As Object, e As RoutedEventArgs) Handles BtnPillButtons.Click
        LengkoAlert.Fire(New AlertOptions With {
            .Title = "Pill / Oval Buttons",
            .Text = "Tombol bergaya modern oval/pill dengan radius lengkungan penuh.",
            .Icon = AlertIcon.Success,
            .OvalButtons = True,
            .ShowDenyButton = True,
            .DenyButtonText = "Tolak",
            .ShowCancelButton = True,
            .CancelButtonText = "Batal",
            .ConfirmButtonText = "Setuju",
            .ConfirmButtonColor = Color.FromRgb(236, 72, 153)
        })
    End Sub

    Private Sub BtnBackdropShake_Click(sender As Object, e As RoutedEventArgs) Handles BtnBackdropShake.Click
        LengkoAlert.Fire(New AlertOptions With {
            .Title = "Klik Luar Dilarang!",
            .Text = "Klik area gelap di luar modal ini untuk melihat animasi getar (Shake).",
            .Icon = AlertIcon.Warning,
            .AllowOutsideClick = False,
            .ConfirmButtonText = "Saya Paham"
        })
    End Sub

    Private Sub BtnRadioSelect_Click(sender As Object, e As RoutedEventArgs) Handles BtnRadioSelect.Click
        Dim res = LengkoAlert.Fire(New AlertOptions With {
            .Title = "Pilih Metode Pembayaran",
            .Input = AlertInputType.Radio,
            .InputOptions = New String() {"Transfer Bank / Virtual Account", "QRIS / GoPay / OVO", "Kartu Kredit / Debit"},
            .ShowCancelButton = True,
            .ConfirmButtonText = "Bayar Sekarang"
        })

        If res.IsConfirmed AndAlso Not String.IsNullOrEmpty(res.Value) Then
            LengkoAlert.ShowToast(String.Format("Metode dipilih: {0}", res.Value), AlertIcon.Success)
        End If
    End Sub
End Class
