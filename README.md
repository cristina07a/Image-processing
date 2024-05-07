<h1>Image processing</h1>

<h2>Overall view</h2>
I used <b>Visual Basic.NET</b> in order to create a program that allows applying filters to images and resizing them. I also used <b>AForge.Imaging v2.2.5</b> from NuGet.
  
The program works as follows: you can add images that will be memorised as <i>layers</i>. Layers can be modified (sepia, grayscale, blur, resizing - depending on canvas size). Images are saved locally, in D:/Facultate/ProiectAPD/image.bmp (tyou can modify this in Form1.vb by setting a different path to bitmap.Save("D:/Facultate/ProiectAPD/image.bmp")).



<h3><b>FIRST VARIANT</b> - Version 1 + Version 2</h3>

There is a difference between the folders I added in this project. The first folder, <b>version 1</b>, uses the library <b>AForge.Imaging</b> in order to blur and grayscale the layers. The folder <b>version 2</b> has a different approach surrounding them, being non-standard (no library). I added this version because it allows for easier implementation of different methods of parallelization.

<h3><b>SECONT VARIANT</b> - threads</h3>

The first method of parallelization I implemented is using <b>threads</b>. The functions necessary for this were added by importhing the <b>System.Threading.Tasks</b> library.
In order for a thread to start its execution, I used <b>thread.Start</b>. This was used for the buttons corresponding to the desired filter (BlurBtn_Click, SepiaBtn_Click, GrayscaleBtn_Click). For every single one of them, there is a new bitmap created inside a thread (declared with <b>Threading.Thread(Sub()... End Sub)</b>, which then has its pixels updated based on a specific formula. Also, inside the Sub, <b>Invoke</b> is used. Its purpose is for updating the image from the main thread.

The way of applying filters, implicitly updating pixels based on formulas, is found in the following functions: GrayscaleImage, ApplySepiaEffect and GaussianBlur. There, I also implemented multithreading. In every single one of them, there is a <b>Parralel.For</b>, which is used for iterating through the bitmap in parallel

Outside of these functions specific for multithreading, I also used <b>LockBits()</b>, which has the role of significantly improving the application (by allowing direct access to pixels data) and preventing concurrent access (it solves the error System.InvalidOperationException: 'Object is currently in use elsewhere.').


<h2>Additional info</h2>

  I ran the code on my laptop, which has 16GB RAM, Intel Core i5.

<h3>Application screenshots</h3>
![image](https://github.com/cristina07a/Image-processing/assets/122676393/173eaa97-20f8-4886-a4c3-7199fb6d1192)



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

