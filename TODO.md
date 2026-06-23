# TODO — NgapainRibet.Rapor

Lacak progres di sini. Karena Core (F#) dikembangkan di Mac/VSCode dan
UI (VB.NET) dikembangkan terpisah di Windows, file ini jadi sumber
kebenaran tunggal soal apa yang sudah & belum dikerjakan — supaya
status kedua sisi tidak hilang saat pindah mesin.

Centang `[x]` kalau sudah selesai & sudah diverifikasi build/run di
mesin yang relevan (Mac untuk Core, Windows untuk UI).

---

## 0. Scaffolding

- [x] Struktur solusi (.sln) + kedua project (Core .fsproj, UI .vbproj)
- [x] Reference UI → Core terpasang
- [x] `dotnet restore` + `dotnet build` untuk **Core** — diverifikasi di Mac (sebelum LLamaSharp ditambahkan)
- [x] `dotnet restore` + `dotnet build` untuk **Core** dengan LLamaSharp — belum diverifikasi ulang setelah `AiEngine.fs` ditambahkan ==> all good, no errors
- [x] `dotnet test` untuk project test (`NgapainRibet.Rapor.Core.Tests`) — belum diverifikasi ==> all good, no errors
- [ ] `dotnet restore` + `dotnet build` untuk **UI** — belum diverifikasi (perlu Windows)
- [ ] Build solusi penuh (`dotnet build NgapainRibet.Rapor.sln`) dari root — belum diverifikasi

## 1. Core (F#) — dikerjakan di Mac/VSCode

> **Catatan review (22 Juni)**: beberapa hal di bawah ini ditemukan saat
> review kode yang sudah di-push, lalu diperbaiki di sesi ini:
> - `ModelManager.fs`: `Completed` sebelumnya dilaporkan SEBELUM verifikasi
>   SHA256 — sudah diperbaiki supaya urutan benar (verifikasi dulu, baru
>   lapor Completed). File yang gagal verifikasi sekarang juga dihapus
>   supaya tidak "dipercaya" di percobaan berikutnya.
> - `ModelManagerTests.fs`: tadinya download ulang ~1.1GB + hapus cache
>   di SETIAP `dotnet test` — sekarang di-`Skip` default (manual saja) &
>   tidak menghapus cache setelah sukses.
> - `AiEngine.fs`: `generateAsync` tadinya mengembalikan error sebagai
>   string biasa (bisa tertukar dengan narasi asli) — sekarang
>   `Result<string, string>`, konsisten dengan `loadModelAsync`.
> - `Student.Notes` vs `buildUserPrompt`'s `additionalNotes` — sudah
>   dikonfirmasi: `Notes` = catatan permanen di profil siswa,
>   `additionalNotes` = catatan sekali pakai untuk generate tertentu.
>   Didokumentasikan di kode + ditambah test untuk keduanya.

- [x] Desain domain model sesungguhnya di `DomainModels.fs`
  - [x] `Student` (Name, Strengths, Weaknesses, Tone)
  - [x] `DownloadState` (NotStarted | Downloading of float | Completed | Error of string)
  - [x] `AiState` (Uninitialized | LoadingModel | Ready | Generating | Failed of string)
- [x] Hapus `ScaffoldPlaceholder` setelah domain model asli ada
- [x] Tambahkan `LLamaSharp` + `LLamaSharp.Backend.Cpu` ke `.fsproj` (versi 0.27.0, cek lagi saat akan restore — versi cepat berubah)
- [x] Logic download & cache model GGUF dari HuggingFace (`ModelManager.fs`)
  - [x] Cek apakah file `.gguf` sudah ada di cache lokal
  - [x] Download dengan progress reporting (`DownloadState`)
  - [x] Verifikasi end-to-end (download sungguhan dari HuggingFace) — belum dicoba ==> berhasil, coba manual di unit tests dengan tambahin `ModelManagerTests.fs`
- [x] Loading model via LLamaSharp (CPU-only, context window kecil 512–1024 token)
  - [x] Kode ditulis di `AiEngine.fs` (`loadModelAsync`)
  - [x] Compile di mac berhasil tanpa ada type mismatch
- [x] Builder prompt sistem dinamis dari checklist (Strengths/Weaknesses/Tone) — pakai istilah Kurikulum Merdeka (Capaian Pembelajaran, Bimbingan, Kompetensi)
  - [x] Kode ditulis & **diuji** (`PromptBuilder.fs` + test xUnit) — ini murni logic, tidak butuh LLamaSharp, jadi confidence tinggi ==> test xUnit berhasil semua
- [x] Fungsi inference (generate narasi) — expose sebagai `Task<string>` / streaming yang gampang dikonsumsi VB.NET
  - [x] Kode ditulis di `AiEngine.fs` (`generateAsync`, streaming lewat `Action<string>`)
  - [x] So far gaada compile error atau build error
- [x] Unit test untuk domain logic & prompt builder (`NgapainRibet.Rapor.Core.Tests`, xUnit)
  - [x] Jalankan `dotnet test` di Mac untuk konfirmasi benar-benar lulus ==> aman semua

## 2. UI (VB.NET WinForms) — dikerjakan di Windows

- [x] Verifikasi scaffold build & run (lihat label status dari Core muncul di Form) ==> 22 juni
- [x] Layar input data siswa (manual)
    - ==> 22 Juni: `InputDataSiswa.vb`, tinggal dicek dan improve code. Jadinya dibuat form terpisah
    - ==> 23 Juni: direview & diperbaiki (autocomplete tone, validasi nama kosong, AcceptButton/CancelButton, baca field cuma saat Simpan).
- [x] Layar konfigurasi per siswa:
  - [x] Pilihan Subject ==> User control
      - ==> 23 Juni: bukan UserControl terpisah (cuma 1 pemakaian) — ComboBox langsung di `GenerateNarasi.vb`, pola sama persis dengan combobox Tone (autocomplete custom source, custom value tetap boleh).
  - [x] Checklist Strengths (Capaian Tertinggi) ==> User control
      - ==> 23 Juni: `Controls/EditableStringListControl.vb` (textbox+Tambah+CheckedListBox+Hapus Terpilih), dipasang di `InputDataSiswa` (bukan di layar generate) karena Strengths adalah field `Student`. Checklist topik tetap dari database TIDAK dibuat (sesuai catatan di bawah, ditunda jadi premium).
  - [x] Checklist Weaknesses (Capaian Terendah) ==> User control
      - ==> 23 Juni: instance kedua dari `EditableStringListControl`, sama seperti Strengths.
  - [x] Pilihan Tone (Sangat Formal / Memotivasi / Tegas tapi Santun) ==> User control
      - ==> sudah ada dari sesi sebelumnya di `InputDataSiswa` (ComboBox custom-value), direview ulang 23 Juni.
  - [x] Catatan guru untuk siswa ini. ==> Text box
      - ==> 23 Juni: `TextBox_CatatanGuru` ditambahkan ke `InputDataSiswa` — sebelumnya `Student.Notes` ga pernah bisa diisi user sama sekali (gap, sudah ditutup).
- [x] Layar progress saat model belum ada (UI untuk `DownloadState`) ==> User control
    - ==> 23 Juni: `Controls/DownloadProgressControl.vb`, render pasif (ProgressBar + status + tombol Batal), dipasang di `GenerateNarasi.vb`.
- [x] Layar progress saat inference berjalan (UI untuk `AiState`) ==> User control
    - ==> 23 Juni: `Controls/InferenceProgressControl.vb`, render pasif (marquee progress + status + tombol Batal), dipasang di `GenerateNarasi.vb`.
- [x] Text box hasil narasi + tombol "Copy to Clipboard"
    - ==> 23 Juni: `RichTextBox_Hasil` + `IconButton_CopyClipboard` di dalam `InferenceProgressControl`, token streaming langsung di-append ke situ.
- [x] Penanganan error (model gagal load, download gagal, dll.) ditampilkan ke user ==> Form custom atau MessageBox?
    - ==> 23 Juni: diputuskan pakai `MessageBox.Show(..., MessageBoxIcon.Error)`, semua dipanggil dari `GenerateNarasi.vb` (lihat tabel error handling di plan).
- [x] Form baru `GenerateNarasi.vb` (orkestrasi Subject + catatan tambahan + Generate + progress + hasil), `MainWindow` sekarang punya lifecycle Engine LLamaSharp (lazy-load sekali, reuse, dispose saat `FormClosing`), tombol utama diganti dari "Uji Coba Input Siswa" jadi "Buat Narasi Rapor" dengan alur nyata: InputDataSiswa → GenerateNarasi.
    - ==> Diverifikasi: `dotnet build NgapainRibet.Rapor.slnx` & `dotnet test tests/NgapainRibet.Rapor.Core.Tests` sukses di Mac (0 error, 17 passed). **BELUM** diverifikasi run sungguhan di Windows (download model asli, generate end-to-end, cek RAM) — lihat catatan verifikasi di plan `/Users/aldi/.claude/plans/gas-ke-todo-berikutnya-joyful-gizmo.md`.

## 3. Premium Features (to be implemented setelah core functionality OK semua)

- [ ] Import data siswa dari Excel (.xlsx) — **DITUNDA**, lihat catatan di bawah
- [ ] Export hasil narasi ke Excel/Word secara bulk — **DITUNDA**, lihat catatan di bawah
- [ ] Export bulk ke Excel/Word


> **Catatan soal Excel import/export**: sengaja ditunda dari sesi ini.
> Ini perlu keputusan library terpisah (mis. ClosedXML untuk Excel,
> DocumentFormat.OpenXml untuk Word) dan testing sendiri — digabung
> sekarang berisiko bikin semuanya tergesa-gesa. Kerjakan di sesi
> berikutnya sebagai unit kerja sendiri.
>> Excel Import/export dijadikan fitur premium yang akan di-paywall nanti.

## 4. Integrasi & Polish (belakangan)

- [ ] Tes end-to-end: input siswa → generate → copy/export, di Windows dengan model sungguhan
- [ ] Cek pemakaian RAM aktual di laptop spek rendah (4–8GB)
- [ ] Packaging/distribusi (exe kecil + first-run model download)
- [ ] Dokumentasi pengguna (cara pakai untuk guru, bukan dokumentasi teknis)

---

## Cara update file ini

Setiap kali menyelesaikan sesuatu di salah satu mesin (Mac atau Windows),
update checklist yang relevan sebelum lanjut sesi berikutnya, supaya saat
pindah konteks (Mac ↔ Windows ↔ chat ini) status selalu sinkron.

Bagian yang sudah dikerjakan tapi aku ragu dikasih ==> keterangan