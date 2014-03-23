Untuk Josh dan Dariel:

1. Kalo belum install VS 2013 Professional, install-lah, soalnya dia ngedukung extension yang bagus"
   (contoh: PowerTools, NuGet Package Manager)
   dan bisa langsung connect ke Git (yang Express juga bisa, tapi lebih baik pake yang Pro)
2. References kemungkinan perlu di-link ulang ke Interop.SHDocVw.dll yang sudah disertakan (buat report generator),
   serta System.Configuration (kita bakal pake ConfigurationManager buat menangani konfigurasi aplikasi)
3. Sistem akan didukung dengan SQLite, jadi tolong dipelajari ya~
4. Ketika nanti kita buat programnya, banyak kode akan dirombak, jadi bakal banyak berubah dari versi aslinya (versi internet)
   NOTE: 90% kode diambil dari internet, tapi masih buggy

Rencana layout tabel program:
--- ID --- URL --- KeyWord1 --- KeyWord2 --- ... --- KeyWordN ---
--- (int) --- (string) --- (boolean) --- (boolean) --- ... --- (boolean) ---

TODO (global):
- ubah regex jadi pake HtmlAgilityPack (Alvin)
- modul Query >> (Josh & Dariel) (pake SQL?)
- bikin GUI untuk Explorer & Crawler, termasuk untuk pengaturan konfigurasinya
- Kaitkan program dengan SQLite >> List?
- algoritma BFS (Dariel)
- cek: apakah bisa pake HtmlAgilityPack buat menangani class Page?
- (bonus round) >> pake multithread untuk pemrosesan, biar lebih cepat