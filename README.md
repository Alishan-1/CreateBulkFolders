# CreateBulkFolders
This software answers the question: How to Create Multiple Folders at Once in Windows?


# Working Description
This program is used to create bulk folders from a list of folder names. The folder names are read from a text file which is provided as an input.  
The program has two modes. 
1- create all folders in a single parent folder (book name.)
2- create folders within multiple provided parent folder names.

in first mode the input file should contain each folder name in a new line as provided in the file: [CreatBulkFolders/example_input_1_simple_list.txt](CreatBulkFolders/example_input_1_simple_list.txt)

in second mode the input file format is as follows: 

folder_name_1, parent1

folder_name_2, parent1

folder_name_4, parent2


as provided in the file: [CreatBulkFolders/example_input_2_book_wise.txt](CreatBulkFolders/example_input_2_book_wise.txt). In this mode all folders with same parent name will be created inside that parent folder. 

The input file and the output folders will be present in the same folder in which the program is present.


### example run mode 1

![mode 1](https://user-images.githubusercontent.com/43717880/153648681-900370cf-b7d0-4b30-8088-9097f2a49447.png)


### example run mode 2

![parent folder wise](https://user-images.githubusercontent.com/43717880/153648676-91bfdf4e-0c2c-4288-bc90-7138902362c6.png)
