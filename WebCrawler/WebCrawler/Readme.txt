Untuk Josh dan Dariel:

1. Kalo belum install VS 2013 Professional, install-lah, soalnya dia ngedukung extension yang bagus"
   (contoh: PowerTools, NuGet Package Manager)
   dan bisa langsung connect ke Git (yang Express juga bisa, tapi lebih baik pake yang Pro)
2. References kemungkinan perlu di-link ulang ke Interop.SHDocVw.dll yang sudah disertakan (buat report generator),
   serta System.Configuration (kita bakal pake ConfigurationManager (App.Config) buat menangani konfigurasi aplikasi)
3. Sistem akan didukung dengan SQLite, jadi tolong dipelajari ya~
4. Ketika nanti kita buat programnya, banyak kode akan dirombak, jadi bakal banyak berubah dari versi aslinya (versi internet)
   NOTE: kode sudah sebagian besar dirombak

Rencana skema tabel program:
--- URL --- Title --- KeyWord1 --- KeyWord2 --- ... --- KeyWordN ---
--- (string) --- (string) --- (boolean) --- (boolean) --- ... --- (boolean) ---

asdf

TODO (global):
- modul Query >> (Josh & Dariel) (pake SQL?)
- bikin GUI untuk Explorer & Crawler, termasuk untuk pengaturan konfigurasinya
- Lengkapi textfile ignored.txt (nanti di-rename), ignored_pages.txt, dan domains.txt
- Kaitkan program dengan SQLite >> List?
- algoritma BFS (Dariel)
- (bonus round) >> pake multithread untuk pemrosesan, biar lebih cepat
- (bonus round) >> cari cara penanganan PHP redirect, biar ngga usah nge-parse webpage yang berupa login, signup, etc.