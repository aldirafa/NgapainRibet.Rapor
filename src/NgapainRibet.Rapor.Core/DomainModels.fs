namespace NgapainRibet.Rapor.Core

/// <summary>
/// Model domain inti untuk NgapainRibet.Rapor (lihat spesifikasi proyek
/// bagian 4). Sengaja dibuat immutable & memakai Discriminated Union
/// supaya transisi status (download, status AI) aman secara tipe —
/// tidak mungkin merepresentasikan status yang tidak valid.
/// </summary>
module DomainModels =

    /// Status proses download model GGUF dari HuggingFace ke cache lokal.
    type DownloadState =
        /// Belum mulai download sama sekali.
        | NotStarted
        /// Sedang download. Progress dalam rentang 0.0 - 1.0.
        | Downloading of progress: float
        /// Download selesai, file siap dipakai.
        | Completed
        /// Download gagal, berisi pesan error untuk ditampilkan ke user.
        | Error of message: string

    /// Status engine AI (LLamaSharp) — dipakai UI untuk menampilkan
    /// indikator yang sesuai (loading spinner, tombol disabled, dst).
    type AiState =
        /// Model belum dimuat ke memori.
        | Uninitialized
        /// Model sedang dimuat (LLamaWeights.LoadFromFileAsync).
        | LoadingModel
        /// Model siap menerima permintaan inference.
        | Ready
        /// Sedang menghasilkan narasi (inference berjalan).
        | Generating
        /// Gagal memuat model atau gagal saat inference.
        | Failed of message: string

    /// Data seorang siswa untuk satu kali proses generate narasi rapor.
    /// `Strengths` dan `Weaknesses` berisi nama topik/Capaian Pembelajaran
    /// yang dipilih guru lewat checklist di UI.
    type Student =
        {
            /// Nama siswa, dipakai untuk menyesuaikan narasi rapor.
            Name: string
            /// Daftar topik/Capaian Pembelajaran yang dipilih guru sebagai kelebihan siswa.
            Strengths: string list
            /// Daftar topik/Capaian Pembelajaran yang dipilih guru sebagai kekurangan siswa.
            Weaknesses: string list
            /// Nada/Style narasi rapor yang dipilih guru (misal: "formal", "humoris", dst).
            Tone: string
            /// Catatan permanen tentang siswa ini, disimpan sebagai bagian dari
            /// profil siswa (bukan sekali pakai). Beda dengan `additionalNotes`
            /// di `PromptBuilder.buildUserPrompt`, yang khusus untuk satu kali
            /// proses generate tertentu saja dan tidak disimpan di profil siswa.
            Notes: string
        }
