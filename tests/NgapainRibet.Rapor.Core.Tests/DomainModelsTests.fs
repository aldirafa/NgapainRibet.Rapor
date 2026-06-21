module NgapainRibet.Rapor.Core.Tests.DomainModelsTests

open Xunit
open NgapainRibet.Rapor.Core.DomainModels

[<Fact>]
let ``DownloadState Downloading menyimpan progress dengan benar`` () =
    let state = Downloading 0.42

    match state with
    | Downloading progress -> Assert.Equal(0.42, progress, 3)
    | _ -> Assert.True(false, "Seharusnya match ke Downloading")

[<Fact>]
let ``DownloadState mendukung equality structural`` () =
    Assert.Equal(NotStarted, NotStarted)
    Assert.Equal(Completed, Completed)
    Assert.NotEqual(Downloading 0.1, Downloading 0.2)
    Assert.Equal(Error "gagal", Error "gagal")

[<Fact>]
let ``AiState Failed menyimpan pesan error`` () =
    let state = Failed "model tidak ditemukan"

    match state with
    | Failed message -> Assert.Equal("model tidak ditemukan", message)
    | _ -> Assert.True(false, "Seharusnya match ke Failed")

[<Fact>]
let ``Student record bisa dibuat dengan field sesuai spesifikasi`` () =
    let student =
        { Name = "Siti Aminah"
          Strengths = [ "Aljabar"; "Geometri" ]
          Weaknesses = [ "Statistika" ]
          Tone = "Memotivasi"
          Notes = "Siti sangat aktif di kelas dan selalu bersemangat dalam belajar." }

    Assert.Equal("Siti Aminah", student.Name)
    Assert.Equal<string list>([ "Aljabar"; "Geometri" ], student.Strengths)
    Assert.Equal<string list>([ "Statistika" ], student.Weaknesses)
    Assert.Equal("Memotivasi", student.Tone)
    Assert.Equal("Siti sangat aktif di kelas dan selalu bersemangat dalam belajar.", student.Notes)

[<Fact>]
let ``Student dengan Strengths dan Weaknesses kosong tetap valid`` () =
    let student =
        { Name = "Budi"
          Strengths = []
          Weaknesses = []
          Tone = "Sangat Formal"
          Notes = "" }

    Assert.Empty(student.Strengths)
    Assert.Empty(student.Weaknesses)
    Assert.Equal("", student.Notes)
