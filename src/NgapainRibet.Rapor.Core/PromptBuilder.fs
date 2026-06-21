namespace NgapainRibet.Rapor.Core

open System
open System.Text
open NgapainRibet.Rapor.Core.DomainModels

/// <summary>
/// Membangun prompt (system + user) untuk LLM berdasarkan data siswa,
/// memakai istilah resmi Kurikulum Merdeka (Capaian Pembelajaran,
/// Bimbingan, Kompetensi). Modul ini sengaja berisi fungsi murni saja
/// (tanpa I/O) supaya mudah di-unit-test dan tidak bergantung pada
/// LLamaSharp atau apa pun yang butuh model di-load.
/// </summary>
module PromptBuilder =

    /// <summary>
    /// Gabungkan daftar topik jadi satu kalimat enak dibaca.
    /// Contoh: ["Aljabar"; "Geometri"] -> "Aljabar dan Geometri"
    /// </summary>
    /// <param name="topics">Daftar topik.</param>
    /// <returns>Kalimat yang menggabungkan semua topik.</returns>
    let private joinTopics (topics: string list) : string =
        match topics with
        | [] -> ""
        | [ single ] -> single
        | _ ->
            let allBustLast = topics |> List.take (topics.Length - 1) |> String.concat ", "
            let last = topics |> List.last
            $"{allBustLast}, dan {last}"

    /// <summary>
    /// Bangun system prompt dinamis dari checklist Strengths/Weaknesses/Tone.
    /// `subject` adalah mata pelajaran (mis. "Matematika").
    /// </summary>
    /// <param name="subject">Mata pelajaran.</param>
    /// <param name="student">Data siswa.</param>
    /// <returns>System prompt yang dibangun.</returns>
    let buildSystemPrompt (subject: string) (student: Student) : string =
        let strengthsText =
            if List.isEmpty student.Strengths then
                "belum ada Capaian Pembelajaran yang menonjol secara khusus tercatat"
            else
                $"menunjukkan Capaian Pembelajaran yang kuat pada {joinTopics student.Strengths}"

        let weaknessesText =
            if List.isEmpty student.Weaknesses then
                "belum ada area yang memerlukan bimbingan khusus saat ini"
            else
                $"masih memerlukan bimbingan lebih lanjut pada {joinTopics student.Weaknesses}"

        let notesText =
            if String.IsNullOrWhiteSpace student.Notes then
                ""
            else
                $"Catatan tambahan dari guru terkait siswa ini: {student.Notes}"

        let sb = StringBuilder()

        sb.AppendLine
            "Anda adalah asistem guru yang membantu menyusun deskripsi Capaian Kompetensi pada rapor siswa, sesuai ketentuan Kurikulum Merdeka yang berlaku di Indonesia."
        |> ignore

        sb.AppendLine $"Mata pelajaran: {subject}." |> ignore

        sb.AppendLine $"Gunakan nada penulisan {student.Tone} dalam menyusun narasi rapor."
        |> ignore

        sb.AppendLine "Aturan penulisan:" |> ignore

        sb.AppendLine
            "* Tulis 2-3 kalimat narasi dalam Bahasa Indonesia sesuai EYD V terbaru, dengan memperhatikan tanda baca, ejaan, dan struktur kalimat yang baik serta personal."
        |> ignore

        sb.AppendLine "* Jangan gunakan format daftar/bullet, tulis sebagai paragraf."
        |> ignore

        sb.AppendLine "* Jangan mengulang nama siswa lebih dari sekali." |> ignore

        sb.AppendLine "* Fokus pada kompetensi yang dicapai dan saran konstruktif, bukan menghakimi."
        |> ignore

        sb.AppendLine $"Siswa ini {strengthsText}, dan {weaknessesText}." |> ignore

        if not (String.IsNullOrWhiteSpace notesText) then
            sb.AppendLine notesText |> ignore

        sb.ToString()

    /// <summary>
    /// Bangun user prompt — pemicu singkat untuk LLM mulai menulis narasi.
    /// </summary>
    /// <param name="student">Data siswa.</param>
    /// <param name="additionalNotes">Catatan tambahan dari guru.</param>
    /// <returns>User prompt yang dibangun.</returns>
    let buildUserPrompt (student: Student) (additionalNotes: string option) : string =
        let r =
            $"Tuliskan deskripsi Capaian Kompetensi untuk {student.Name} sesuai instruksi di atas."

        match additionalNotes with
        | Some notes when not (String.IsNullOrWhiteSpace notes) -> $"{r} {Environment.NewLine}Catatan tambahan: {notes}"
        | _ -> r
