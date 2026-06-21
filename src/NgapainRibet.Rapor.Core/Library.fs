namespace NgapainRibet.Rapor.Core

/// <summary>
/// Titik masuk sementara untuk Core. Berisi fungsi sederhana
/// untuk memverifikasi bahwa UI (VB.NET) bisa memanggil Core (F#)
/// dengan benar. Logic AI/inference sesungguhnya akan ditambahkan
/// di sini pada langkah pengembangan berikutnya.
/// </summary>
module CoreInfo =

    /// Versi engine Core. Dipakai UI untuk menampilkan info build,
    /// dan sebagai pengecekan sederhana bahwa reference project sudah benar.
    let engineVersion : string = "0.1.0-scaffold"

    /// Contoh fungsi murni F# yang dipanggil dari VB.NET, untuk
    /// membuktikan boundary interop berfungsi sebelum logic
    /// sesungguhnya (prompt building, AI inference) ditambahkan.
    let getStatusMessage () : string =
        sprintf "NgapainRibet.Rapor.Core siap (versi %s)" engineVersion
