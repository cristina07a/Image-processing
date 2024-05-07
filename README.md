<h1>Image processing</h1>

<h2>Overview</h2>
I used <b>Visual Basic.NET</b> in order to create a program that allows applying filters to images and resizing them. I also used <b>AForge.Imaging v2.2.5</b> from NuGet and <b>System.Threading.Tasks</b>

The program works as follows: you can add images that will be memorised as <i>layers</i>. Layers can have filters applied to them (sepia, grayscale, blur, resizing - depending on canvas size). Images are saved locally, in D:/Facultate/ProiectAPD/image.bmp (tyou can modify this in Form1.vb by setting a different path to bitmap.Save("D:/Facultate/ProiectAPD/image.bmp")).

<h4><b>FIRST VARIANT</b> - Version 1 + Version 2</h4>

There is a difference between the folders I added in this project. The first folder, <b>version 1</b>, uses the library <b>AForge.Imaging</b> in order to blur and grayscale the layers. The folder <b>version 2</b> has a different approach surrounding them, being non-standard (no library). I added this version because it allows for easier implementation of different methods of parallelization.

<h4><b>SECONT VARIANT</b> - threads</h4>

The first method of parallelization I implemented is using <b>threads</b>. The functions necessary for this were added by importhing the <b>System.Threading.Tasks</b> library.
In order for a thread to start its execution, I used <b>thread.Start</b>. This was used for the buttons corresponding to the desired filter (BlurBtn_Click, SepiaBtn_Click, GrayscaleBtn_Click). For every single one of them, there is a new bitmap created inside a thread (declared with <b>Threading.Thread(Sub()... End Sub)</b>, which then has its pixels updated based on a specific formula. Also, inside the Sub, <b>Invoke</b> is used. Its purpose is for updating the image from the main thread.

The way of applying filters, implicitly updating pixels based on formulas, is found in the following functions: GrayscaleImage, ApplySepiaEffect and GaussianBlur. There, I also implemented multithreading. In GrascaleImage, there is a <b>Parralel.For</b>, which is used for iterating through the bitmap in parallel

Outside of these functions specific for multithreading, I also used <b>LockBits()</b>, which has the role of significantly improving the application (by allowing direct access to pixels data) and preventing concurrent access (it solves the error System.InvalidOperationException: 'Object is currently in use elsewhere.').


<h2>Additional info</h2>
To see the duration required for applying a filter, I added a new Layer on right bottom where I display it in milliseconds. I used a stopwatch for every button that is responsible for a filter before and right after applying it.

Machine specifications:
Processor: Intel(R) Core(TM) i5-9300H CPU @ 2.40GHz   2.40 GHz
RAM: 16GB

<h4>First variant – version 1:</h4>
Grayscale - average 50ms<br>
Sepia - average 60ms<br>
Blur - average 500ms<br>
<h4>First variant – version 2:</h4>
Grayscale - average 55ms<br>
Sepia - average 60ms<br>
Blur - average 520ms<br>
<h4>Second variant – threads:</h4>
Grayscale - average 5ms<br>
Sepia - average 55ms<br>
Blur - average 500ms<br>


<h3>Application screenshots</h3>

![image](https://github.com/cristina07a/Image-processing/assets/122676393/41e3ba72-e87d-47d9-83a9-fd02f3f977aa)


add image:
![image](https://github.com/cristina07a/Image-processing/assets/122676393/67d8e032-ed74-467f-a983-8ee6e587d552)

resizing:
![image](https://github.com/cristina07a/Image-processing/assets/122676393/bf59ca6c-da96-43bd-9e75-db66fe366cd2)

grayscale:
![image](https://github.com/cristina07a/Image-processing/assets/122676393/909bf41b-e5e2-4730-bc5c-24ca63cc62f4)

blur:
![image](https://github.com/cristina07a/Image-processing/assets/122676393/e8606ebf-e410-4d1e-bf25-7ef5bb0f6a81)

sepia:
![image](https://github.com/cristina07a/Image-processing/assets/122676393/03195256-972b-4613-a1d7-8fdff3bb410b)

