<h1>Image processing</h1>

<h2>Overall view</h2>
I used <b>Visual Basic.NET</b> in order to create a program that allows applying filters to images and resizing them. I also used <b>AForge.Imaging v2.2.5</b> from NuGet.
  
The program works as follows: you can add images that will be memorised as <i>layers</i>. Layers can be modified (sepia, grayscale, blur, resizing - depending on canvas size). Images are saved locally, in D:/Facultate/ProiectAPD/image.bmp (the path can be modified in Form1.vb, line 28).

There is a difference between the folders I added in this project. The first folder, <b>version 1</b>, uses the library AForge.Imaging in order to blur and grayscale the layers. The folder <b>version 2</b> has a different take surrounding them, being non-standard (no library).

<h2>Info</h2>

  I ran the code on my laptop, which has 16GB RAM, Intel Core i5.

asa arata interfata:
![image](https://github.com/cristina07a/Image-processing/assets/122676393/9d108055-6a6a-4527-9b65-2c28175cf567)

adaugare imagine:
![image](https://github.com/cristina07a/Image-processing/assets/122676393/f8357876-72d0-4b57-96df-976826816f6c)

resizing:
![image](https://github.com/cristina07a/Image-processing/assets/122676393/85734470-bbc6-4cd2-9538-4bfd70635510)

grayscale:
![image](https://github.com/cristina07a/Image-processing/assets/122676393/5153fd35-6c30-4712-a605-e666db0f114f)

blur:
![image](https://github.com/cristina07a/Image-processing/assets/122676393/51d94c82-7cc6-4ebd-a2a7-56370ee2b73f)

sepia:
![image](https://github.com/cristina07a/Image-processing/assets/122676393/5d309429-6ec5-40d5-94f2-5a52c9098a80)

