namespace Lengko.Alert.Wpf.Demo.FSharp

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open Lengko.Alert.Wpf

module Program =

    type MainWindow() as this =
        inherit Window()

        let mutable clickCount = 0

        let makeButton (text: string) (bgHex: string) (onClick: RoutedEventArgs -> unit) =
            let color = ColorConverter.ConvertFromString(bgHex) :?> Color
            let btn = Button(
                Content = text,
                Background = SolidColorBrush(color),
                Foreground = Brushes.White,
                FontWeight = FontWeights.SemiBold,
                FontSize = 12.5,
                Padding = Thickness(12.0, 6.0, 12.0, 6.0),
                Margin = Thickness(0.0, 0.0, 8.0, 8.0),
                BorderThickness = Thickness(0.0),
                Cursor = Input.Cursors.Hand
            )
            btn.Click.Add(onClick)
            btn

        let makeCard (title: string) (buttons: Button list) =
            let border = Border(
                Background = Brushes.White,
                BorderBrush = SolidColorBrush(Color.FromRgb(226uy, 232uy, 240uy)),
                BorderThickness = Thickness(1.0),
                CornerRadius = CornerRadius(8.0),
                Padding = Thickness(16.0),
                Margin = Thickness(8.0),
                Width = 430.0
            )
            let panel = StackPanel()
            let titleBlock = TextBlock(
                Text = title,
                FontWeight = FontWeights.Bold,
                FontSize = 15.0,
                Foreground = SolidColorBrush(Color.FromRgb(30uy, 41uy, 59uy)),
                Margin = Thickness(0.0, 0.0, 0.0, 12.0)
            )
            panel.Children.Add(titleBlock) |> ignore

            let wrap = WrapPanel()
            for b in buttons do
                wrap.Children.Add(b) |> ignore

            panel.Children.Add(wrap) |> ignore
            border.Child <- panel
            border

        do
            this.Title <- "Lengko.Alert.Wpf — F# Demo"
            this.Width <- 980.0
            this.Height <- 760.0
            this.MinWidth <- 750.0
            this.MinHeight <- 600.0
            this.WindowStartupLocation <- WindowStartupLocation.CenterScreen
            this.Background <- SolidColorBrush(Color.FromRgb(248uy, 250uy, 252uy))
            this.FontFamily <- FontFamily("Segoe UI")
            this.FontSize <- 13.0

            let rootGrid = Grid(Margin = Thickness(20.0))
            rootGrid.RowDefinitions.Add(RowDefinition(Height = GridLength.Auto))
            rootGrid.RowDefinitions.Add(RowDefinition(Height = GridLength(1.0, GridUnitType.Star)))
            rootGrid.RowDefinitions.Add(RowDefinition(Height = GridLength.Auto))

            // Header
            let header = StackPanel(Margin = Thickness(8.0, 0.0, 0.0, 16.0))
            let hTitle = TextBlock(
                Text = "Lengko.Alert Demo (F# WPF)",
                FontSize = 22.0,
                FontWeight = FontWeights.Bold,
                Foreground = SolidColorBrush(Color.FromRgb(15uy, 23uy, 42uy))
            )
            let hSub = TextBlock(
                Text = "Zero external dependencies. Pure WPF hardware-accelerated vector alerts, toasts, and prompts.",
                FontSize = 13.5,
                Foreground = SolidColorBrush(Color.FromRgb(100uy, 116uy, 139uy)),
                Margin = Thickness(0.0, 3.0, 0.0, 0.0)
            )
            header.Children.Add(hTitle) |> ignore
            header.Children.Add(hSub) |> ignore
            Grid.SetRow(header, 0)
            rootGrid.Children.Add(header) |> ignore

            // ScrollViewer Cards
            let scroller = ScrollViewer(VerticalScrollBarVisibility = ScrollBarVisibility.Auto)
            let cardsWrap = WrapPanel(Orientation = Orientation.Horizontal)

            // Card 1: Basic Modal Alerts
            let card1 = makeCard "1. Basic Modal Alerts" [
                makeButton "Success Alert" "#22C55E" (fun _ ->
                    LengkoAlert.Fire("Berhasil!", "Data tersimpan dengan sukses ke database.", AlertIcon.Success) |> ignore)
                makeButton "Error Alert" "#EF4444" (fun _ ->
                    LengkoAlert.Fire("Oops...", "Terjadi kesalahan saat memproses data.", AlertIcon.Error) |> ignore)
                makeButton "Warning Alert" "#F59E0B" (fun _ ->
                    LengkoAlert.Fire("Peringatan!", "Sesi login Anda akan segera berakhir dalam 5 menit.", AlertIcon.Warning) |> ignore)
                makeButton "Info Alert" "#3B82F6" (fun _ ->
                    LengkoAlert.Fire("Informasi", "Lengko.Alert.Wpf berjalan murni tanpa dependensi eksternal.", AlertIcon.Info) |> ignore)
                makeButton "Question Alert" "#8B5CF6" (fun _ ->
                    LengkoAlert.Fire("Pertanyaan", "Apakah Anda ingin melanjutkan sinkronisasi data sekarang?", AlertIcon.Question) |> ignore)
            ]
            cardsWrap.Children.Add(card1) |> ignore

            // Card 2: Confirm & Prompt
            let card2 = makeCard "2. Confirm & Prompt" [
                makeButton "Confirm (Sync)" "#4F46E5" (fun _ ->
                    let ok = LengkoAlert.Confirm("Konfirmasi Hapus", "Yakin ingin menghapus record ini secara permanen?", "Ya, Hapus!", "Batal", AlertIcon.Warning)
                    if ok then
                        LengkoAlert.Fire("Terhapus!", "Record berhasil dihapus dari sistem.", AlertIcon.Success) |> ignore)
                makeButton "Confirm (Async)" "#4F46E5" (fun _ ->
                    async {
                        let! ok = LengkoAlert.ConfirmAsync("Konfirmasi Logout", "Apakah Anda yakin ingin keluar dari aplikasi?", "Logout", "Batal", AlertIcon.Question) |> Async.AwaitTask
                        if ok then
                            LengkoAlert.ShowToast("Anda telah logout.", AlertIcon.Info, 3000)
                    } |> Async.StartImmediate)
                makeButton "Prompt Text" "#0284C7" (fun _ ->
                    let name = LengkoAlert.Prompt("Input Nama Lengkap", "Nama Anda...", "Caswandi", null)
                    if not (String.IsNullOrEmpty(name)) then
                        LengkoAlert.Fire("Halo!", sprintf "Selamat datang, %s!" name, AlertIcon.Success) |> ignore)
                makeButton "Prompt (Async)" "#0284C7" (fun _ ->
                    async {
                        let! email = LengkoAlert.PromptAsync("Input Email", "email@contoh.com", "", null) |> Async.AwaitTask
                        if not (String.IsNullOrEmpty(email)) then
                            LengkoAlert.ShowToast(sprintf "Email '%s' tersimpan!" email, AlertIcon.Success, 3000)
                    } |> Async.StartImmediate)
                makeButton "Prompt + Validator" "#0D9488" (fun _ ->
                    let validator = Func<string, string>(fun v ->
                        if String.IsNullOrWhiteSpace(v) then "PIN tidak boleh kosong!"
                        elif v.Length < 6 then "PIN minimal harus 6 karakter!"
                        else null)
                    let pin = LengkoAlert.Prompt("Masukkan PIN Keamanan", "Minimal 6 digit...", "", validator)
                    if not (String.IsNullOrEmpty(pin)) then
                        LengkoAlert.Fire("Akses Diberikan", "PIN valid!", AlertIcon.Success) |> ignore)
            ]
            cardsWrap.Children.Add(card2) |> ignore

            // Card 3: Corner Toasts
            let card3 = makeCard "3. Corner Toasts (Non-blocking)" [
                makeButton "Toast Success (Top-End)" "#10B981" (fun _ ->
                    LengkoAlert.ShowToast("Sinkronisasi Selesai!", AlertIcon.Success, 3000))
                makeButton "Toast Warning (Top-Start)" "#F59E0B" (fun _ ->
                    LengkoAlert.ShowToast("Koneksi Tidak Stabil", "Mencoba menghubungkan kembali...", AlertPosition.TopStart, AlertIcon.Warning, 3000))
                makeButton "Toast Error (Bottom-End)" "#EF4444" (fun _ ->
                    LengkoAlert.ShowToast("Gagal Mengunduh File", "Server timeout (504)", AlertPosition.BottomEnd, AlertIcon.Error, 3000))
                makeButton "Toast Info (Bottom-Center)" "#3B82F6" (fun _ ->
                    LengkoAlert.ShowToast("Pembaruan Tersedia", "Versi 1.0.0 siap diunduh.", AlertPosition.Bottom, AlertIcon.Info, 3000))
                makeButton "Custom 5s Toast" "#64748B" (fun _ ->
                    let opt = AlertOptions()
                    opt.Title <- "Notifikasi Custom"
                    opt.Text <- "Toast aktif selama 5 detik dengan animasi SlideSide."
                    opt.Position <- AlertPosition.TopEnd
                    opt.Icon <- AlertIcon.Success
                    opt.Timer <- 5000
                    opt.TimerProgressBar <- true
                    opt.OpenAnimation <- AlertAnimation.SlideSide
                    LengkoAlert.ShowToast(opt))
            ]
            cardsWrap.Children.Add(card3) |> ignore

            // Card 4: Advanced & Custom Options
            let card4 = makeCard "4. Advanced & Custom Options" [
                makeButton "Dark Theme Modal" "#1E293B" (fun _ ->
                    let opt = AlertOptions()
                    opt.Title <- "Dark Theme Modal"
                    opt.Text <- "Tampilan elegan tema gelap slate dengan kontras tinggi."
                    opt.Icon <- AlertIcon.Info
                    opt.Theme <- AlertTheme.Dark
                    opt.ShowCancelButton <- true
                    opt.CancelButtonText <- "Tutup"
                    opt.ConfirmButtonText <- "Keren!"
                    LengkoAlert.Fire(opt) |> ignore)
                makeButton "Non-Modal Floating Alert" "#0284C7" (fun _ ->
                    let opt = AlertOptions()
                    opt.Title <- "Non-Modal Floating Alert"
                    opt.Text <- "Alert ini mengambang di layar tanpa memblokir interaksi window utama! Coba klik tombol Counter di bawah."
                    opt.Icon <- AlertIcon.Info
                    opt.Modal <- false
                    opt.Backdrop <- false
                    opt.Position <- AlertPosition.CenterEnd
                    opt.ShowConfirmButton <- true
                    opt.ConfirmButtonText <- "Mengerti"
                    LengkoAlert.Fire(opt) |> ignore)
                makeButton "Oval / Pill Buttons" "#EC4899" (fun _ ->
                    let opt = AlertOptions()
                    opt.Title <- "Pill / Oval Buttons"
                    opt.Text <- "Tombol bergaya modern oval/pill dengan radius lengkungan penuh."
                    opt.Icon <- AlertIcon.Success
                    opt.OvalButtons <- true
                    opt.ShowDenyButton <- true
                    opt.DenyButtonText <- "Tolak"
                    opt.ShowCancelButton <- true
                    opt.CancelButtonText <- "Batal"
                    opt.ConfirmButtonText <- "Setuju"
                    opt.ConfirmButtonColor <- Nullable(Color.FromRgb(236uy, 72uy, 153uy))
                    LengkoAlert.Fire(opt) |> ignore)
                makeButton "Backdrop Shake Demo" "#D97706" (fun _ ->
                    let opt = AlertOptions()
                    opt.Title <- "Klik Luar Dilarang!"
                    opt.Text <- "Klik area gelap di luar modal ini untuk melihat animasi getar (Shake)."
                    opt.Icon <- AlertIcon.Warning
                    opt.AllowOutsideClick <- false
                    opt.ConfirmButtonText <- "Saya Paham"
                    LengkoAlert.Fire(opt) |> ignore)
                makeButton "Radio / Select Inputs" "#7C3AED" (fun _ ->
                    let opts = [| "Transfer Bank / Virtual Account"; "QRIS / GoPay / OVO"; "Kartu Kredit / Debit" |]
                    let opt = AlertOptions()
                    opt.Title <- "Pilih Metode Pembayaran"
                    opt.Input <- AlertInputType.Radio
                    opt.InputOptions <- opts
                    opt.ShowCancelButton <- true
                    opt.ConfirmButtonText <- "Bayar Sekarang"
                    let res = LengkoAlert.Fire(opt)
                    if res.IsConfirmed && not (String.IsNullOrEmpty(res.Value)) then
                        LengkoAlert.ShowToast(sprintf "Metode dipilih: %s" res.Value, AlertIcon.Success, 3000))
            ]
            cardsWrap.Children.Add(card4) |> ignore

            scroller.Content <- cardsWrap
            Grid.SetRow(scroller, 1)
            rootGrid.Children.Add(scroller) |> ignore

            // Bottom Responsiveness Test Bar
            let bottomBorder = Border(
                Background = Brushes.White,
                BorderBrush = SolidColorBrush(Color.FromRgb(226uy, 232uy, 240uy)),
                BorderThickness = Thickness(1.0),
                CornerRadius = CornerRadius(8.0),
                Padding = Thickness(14.0),
                Margin = Thickness(8.0, 10.0, 8.0, 0.0)
            )
            let bottomGrid = Grid()
            bottomGrid.ColumnDefinitions.Add(ColumnDefinition(Width = GridLength.Auto))
            bottomGrid.ColumnDefinitions.Add(ColumnDefinition(Width = GridLength(1.0, GridUnitType.Star)))

            let btnCounter = Button(
                Content = "Test UI Responsiveness (Click Count: 0)",
                Background = SolidColorBrush(Color.FromRgb(2uy, 132uy, 199uy)),
                Foreground = Brushes.White,
                FontWeight = FontWeights.SemiBold,
                FontSize = 12.5,
                Padding = Thickness(14.0, 7.0, 14.0, 7.0),
                Margin = Thickness(0.0, 0.0, 14.0, 0.0),
                BorderThickness = Thickness(0.0),
                Cursor = Input.Cursors.Hand
            )
            btnCounter.Click.Add(fun _ ->
                clickCount <- clickCount + 1
                btnCounter.Content <- sprintf "Test UI Responsiveness (Click Count: %d)" clickCount)

            let txtInteractive = TextBox(
                VerticalAlignment = VerticalAlignment.Center,
                Padding = Thickness(8.0, 6.0, 8.0, 6.0),
                Text = "Type here freely while toast or non-modal alert is floating...",
                Foreground = SolidColorBrush(Color.FromRgb(71uy, 85uy, 105uy)),
                Background = SolidColorBrush(Color.FromRgb(241uy, 245uy, 249uy)),
                BorderBrush = SolidColorBrush(Color.FromRgb(203uy, 213uy, 225uy))
            )

            Grid.SetColumn(btnCounter, 0)
            Grid.SetColumn(txtInteractive, 1)
            bottomGrid.Children.Add(btnCounter) |> ignore
            bottomGrid.Children.Add(txtInteractive) |> ignore

            bottomBorder.Child <- bottomGrid
            Grid.SetRow(bottomBorder, 2)
            rootGrid.Children.Add(bottomBorder) |> ignore

            this.Content <- rootGrid

    [<STAThread>]
    [<EntryPoint>]
    let main _ =
        let app = Application()
        app.Run(MainWindow())
