Image processing

Cerinta + ce am folosit: 

  Am implementat un program in Visual Basic.NET care face image processing. Am folosit,de asemenea: AForge.Imaging v2.2.5 (luat prin NuGet). Se pot adauga mai multe imagini care sunt memorate ca layers, 
  iar apoi se pot aplica filtre (sepia, grayscale, blur). Sepia se poate aplica pentru fiecare layer selectat, in timp ce grayscale si blur se aplica tuturor layer-elor. De asemenea, se mai poate modifica
  marimea fiecarui layer (adica a fiecarei imagini adaugate) in functie de lungimea plansei de lucru (Canvas). Imaginile se salveaza local, in D:/Facultate/ProiectAPD/image.bmp (locatia se poate modifica
  din Form1.vb, linia 28.

  Filtrele de grayscale si blur se fac folosindu-se Aforge.Imaging

Info masina:

  Am rulat codul pe laptopul meu, care are 16GB RAM, Intel Core i5.

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



