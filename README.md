# Tubes2_BingChilling
Tugas Besar 2 IF2211 Strategi Algoritma
Pengaplikasian Algoritma BFS dan DFS dalam Implementasi Path Finding

## Daftar Isi
* [Deskripsi Singkat Program](#deskripsi-singkat-program)
* [Requirement Program](#requirement-program)
* [Cara Kompilasi Program](#cara-kompilasi-program)
* [Cara Menjalankan Program](#cara-menjalankan-program)
* [Authors](#authors)
* [Link Demo Program](#link-demo-program)

## Deskripsi Singkat Program

Dalam tugas besar ini, Anda akan diminta untuk membangun sebuah aplikasi GUI sederhana yang dapat memodelkan fitur dari file explorer pada sistem operasi, yang pada tugas ini disebut dengan Folder Crawling. Dengan memanfaatkan algoritma Breadth First Search (BFS) dan Depth First Search (DFS), Anda dapat menelusuri folder-folder yang ada pada direktori untuk mendapatkan direktori yang Anda inginkan. Anda juga diminta untuk memvisualisasikan hasil dari pencarian folder tersebut dalam bentuk pohon.

Langkah awal dalam proses mendesain solusi dari permasalahan ini adalah dengan membagi permasalahan utama, yaitu folder crawling, menjadi beberapa permasalahan kecil sehingga dapat mempermudah pencarian solusi. Permasalahan tersebut dapat dibagi menjadi dua permasalahan utama, yaitu pencarian file menggunakan algoritma breadth-first search dan algoritma depth-first search. Selain itu, ada permasalahan-permasalahan tambahan yang perlu untuk diselesaikan yaitu solusi dari dua permasalahan utama tersebut harus ditampilkan dalam bentuk desktop application.

Langkah awal dari proses memecahkan masalah dengan memecah permasalahan find treasure  ini menjadi bagian yang lebih kecil. Permasalahan kami bagi menjadi dua bagian yakni pencarian solusi menggunakan algoritma breadth first search dan dengan menggunakan depth first search.Selain itu, ada permasalahan lain yakni menampilkan solusi dari dua permasalahan tersebut dalam bentuk desktop application.

Setelah membagi permasalahan menjadi dua bagian, kita harus melakukan pemetaan masalah tersebut agar dapat digunakan oleh masing masing algoritma breadth first search dan dengan menggunakan depth first search. Kami merepresentasikan masalah ke dalam bentuk objek tile .Semua tile tersebut disimpan dalam tiles.

Dalam menyelesaikan masalah tambahan, yakni menampilkan solusi dari masing masing algoritma. Kami menggunakan GUI dalam Windows Presentation Foundadtion yang dapat menerima masukkan pengguna berupa input file, pilihan algoritma (BFS atau DFS) serta pilihan untuk visualisasi TSP. Dari inputan - inputan tersebut, Kami juga menvisualisai mase pada GUI tersebut. Setelah itu, kami menampilkan solusi yakni jalur untuk menemukan treasure dalam maze tersebut. Pada akhirnya, kami melakukan testing  dari GUI yang telah kita buat.

## Requirement Program
    - Windows Operating System
    - .NET
    - Visual Studio 2022
    - MSAGL
    - WPF

## Cara Kompilasi Program
1. Lakukan git clone terhadap repositori ini
2. Buka Solution `Tubes2_BingChilling.sln` dari repositori ini
3. Run program dengan menggunakan tombol Run pada Visual Studio 2022

## Cara Menjalankan Program
1. Buka folder `bin/debug` pada folder repositori
2. Jalankan file `Tubes2_BingChilling.exe`

## Authors

| NIM      | NAMA                        |
|----------|-----------------------------|
| 13521080 | Fajar Maulana H             |
| 13521119 | Muhammad Rizky Sya’ban      |
| 13520152 | Muhammad Naufal Nalendra    |

## Link Demo Program
* [Tugas Besar II IF2211 Strategi Algoritma tahun 2021/2022](bit.ly/BonusVideoTubesStima2)
