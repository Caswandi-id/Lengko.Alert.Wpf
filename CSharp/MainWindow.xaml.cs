using System.Windows;
using System.Windows.Media;
using Lengko.Alert.Wpf;

namespace Lengko.Alert.Demo.Wpf.CSharp;

public partial class MainWindow : Window
{
    private int _clickCount = 0;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnCounter_Click(object sender, RoutedEventArgs e)
    {
        _clickCount++;
        BtnCounter.Content = $"Test UI Responsiveness (Click Count: {_clickCount})";
    }

    // 1. Basic Modal Alerts
    private void BtnSuccess_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.Fire("Berhasil!", "Data tersimpan dengan sukses ke database.", AlertIcon.Success);
    }

    private void BtnError_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.Fire("Oops...", "Terjadi kesalahan saat memproses data.", AlertIcon.Error);
    }

    private void BtnWarning_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.Fire("Peringatan!", "Sesi login Anda akan segera berakhir dalam 5 menit.", AlertIcon.Warning);
    }

    private void BtnInfo_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.Fire("Informasi", "Lengko.Alert.Wpf berjalan murni tanpa dependensi eksternal.", AlertIcon.Info);
    }

    private void BtnQuestion_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.Fire("Pertanyaan", "Apakah Anda ingin melanjutkan sinkronisasi data sekarang?", AlertIcon.Question);
    }

    // 2. Confirm & Prompt
    private void BtnConfirmSync_Click(object sender, RoutedEventArgs e)
    {
        bool ok = LengkoAlert.Confirm(
            "Konfirmasi Hapus",
            "Yakin ingin menghapus record ini secara permanen?",
            confirmText: "Ya, Hapus!",
            cancelText: "Batal",
            icon: AlertIcon.Warning);

        if (ok)
        {
            LengkoAlert.Fire("Terhapus!", "Record berhasil dihapus dari sistem.", AlertIcon.Success);
        }
    }

    private async void BtnConfirmAsync_Click(object sender, RoutedEventArgs e)
    {
        bool ok = await LengkoAlert.ConfirmAsync(
            "Konfirmasi Logout",
            "Apakah Anda yakin ingin keluar dari aplikasi?",
            confirmText: "Logout",
            cancelText: "Batal",
            icon: AlertIcon.Question);

        if (ok)
        {
            LengkoAlert.ShowToast("Anda telah logout.", AlertIcon.Info);
        }
    }

    private void BtnPromptSync_Click(object sender, RoutedEventArgs e)
    {
        string? name = LengkoAlert.Prompt("Input Nama Lengkap", placeholder: "Nama Anda...", defaultValue: "Caswandi");
        if (!string.IsNullOrEmpty(name))
        {
            LengkoAlert.Fire("Halo!", $"Selamat datang, {name}!", AlertIcon.Success);
        }
    }

    private async void BtnPromptAsync_Click(object sender, RoutedEventArgs e)
    {
        string? email = await LengkoAlert.PromptAsync("Input Email", placeholder: "email@contoh.com");
        if (!string.IsNullOrEmpty(email))
        {
            LengkoAlert.ShowToast($"Email '{email}' tersimpan!", AlertIcon.Success);
        }
    }

    private void BtnPromptValidator_Click(object sender, RoutedEventArgs e)
    {
        string? pin = LengkoAlert.Prompt(
            "Masukkan PIN Keamanan",
            placeholder: "Minimal 6 digit...",
            validator: val =>
            {
                if (string.IsNullOrWhiteSpace(val)) return "PIN tidak boleh kosong!";
                if (val.Length < 6) return "PIN minimal harus 6 karakter!";
                return null;
            });

        if (pin != null)
        {
            LengkoAlert.Fire("Akses Diberikan", "PIN valid!", AlertIcon.Success);
        }
    }

    // 3. Corner Toasts
    private void BtnToastSuccess_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.ShowToast("Sinkronisasi Selesai!", AlertIcon.Success);
    }

    private void BtnToastWarning_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.ShowToast("Koneksi Tidak Stabil", "Mencoba menghubungkan kembali...", AlertPosition.TopStart, AlertIcon.Warning);
    }

    private void BtnToastError_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.ShowToast("Gagal Mengunduh File", "Server timeout (504)", AlertPosition.BottomEnd, AlertIcon.Error);
    }

    private void BtnToastInfo_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.ShowToast("Pembaruan Tersedia", "Versi 1.0.0 siap diunduh.", AlertPosition.Bottom, AlertIcon.Info);
    }

    private void BtnToastCustom_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.ShowToast(new AlertOptions
        {
            Title = "Notifikasi Custom",
            Text = "Toast aktif selama 5 detik dengan animasi SlideSide.",
            Position = AlertPosition.TopEnd,
            Icon = AlertIcon.Success,
            Timer = 5000,
            TimerProgressBar = true,
            OpenAnimation = AlertAnimation.SlideSide
        });
    }

    // 4. Advanced & Custom Options
    private void BtnDarkTheme_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.Fire(new AlertOptions
        {
            Title = "Dark Theme Modal",
            Text = "Tampilan elegan tema gelap slate dengan kontras tinggi.",
            Icon = AlertIcon.Info,
            Theme = AlertTheme.Dark,
            ShowCancelButton = true,
            CancelButtonText = "Tutup",
            ConfirmButtonText = "Keren!"
        });
    }

    private void BtnNonModal_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.Fire(new AlertOptions
        {
            Title = "Non-Modal Floating Alert",
            Text = "Alert ini mengambang di layar tanpa memblokir interaksi window utama! Coba klik tombol Counter di bawah.",
            Icon = AlertIcon.Info,
            Modal = false,
            Backdrop = false,
            Position = AlertPosition.CenterEnd,
            ShowConfirmButton = true,
            ConfirmButtonText = "Mengerti"
        });
    }

    private void BtnPillButtons_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.Fire(new AlertOptions
        {
            Title = "Pill / Oval Buttons",
            Text = "Tombol bergaya modern oval/pill dengan radius lengkungan penuh.",
            Icon = AlertIcon.Success,
            OvalButtons = true,
            ShowDenyButton = true,
            DenyButtonText = "Tolak",
            ShowCancelButton = true,
            CancelButtonText = "Batal",
            ConfirmButtonText = "Setuju",
            ConfirmButtonColor = Color.FromRgb(236, 72, 153)
        });
    }

    private void BtnBackdropShake_Click(object sender, RoutedEventArgs e)
    {
        LengkoAlert.Fire(new AlertOptions
        {
            Title = "Klik Luar Dilarang!",
            Text = "Klik area gelap di luar modal ini untuk melihat animasi getar (Shake).",
            Icon = AlertIcon.Warning,
            AllowOutsideClick = false,
            ConfirmButtonText = "Saya Paham"
        });
    }

    private void BtnRadioSelect_Click(object sender, RoutedEventArgs e)
    {
        var res = LengkoAlert.Fire(new AlertOptions
        {
            Title = "Pilih Metode Pembayaran",
            Input = AlertInputType.Radio,
            InputOptions = new[] { "Transfer Bank / Virtual Account", "QRIS / GoPay / OVO", "Kartu Kredit / Debit" },
            ShowCancelButton = true,
            ConfirmButtonText = "Bayar Sekarang"
        });

        if (res.IsConfirmed && !string.IsNullOrEmpty(res.Value))
        {
            LengkoAlert.ShowToast($"Metode dipilih: {res.Value}", AlertIcon.Success);
        }
    }
}
