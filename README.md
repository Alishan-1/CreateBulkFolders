# CreateBulkFolders
This software answers the question: How to Create Multiple Folders at Once in Windows?


# Working Description
This program is used to create bulk folders from a list of folder names. The folder names are read from a text file which is provided as an input.  
The program has two modes. 
1- create all folders in a single parent folder (book name.)
2- create folders within multiple provided parent folder names.

in first mode the input file format is as follows: 

folder_name_1
folder_name_2
folder_name_3
folder_name_4

as provided in the file: CreateBulkFolders/CreatBulkFolders/example_input_1_simple_list.txt

in second mode the input file format is as follows: 

folder_name_1, parent1
folder_name_2, parent1
folder_name_3, parent1
folder_name_4, parent2
folder_name_1, parent2
folder_name_2, parent3
folder_name_3, parent3
folder_name_4, parent3


as provided in the file: CreateBulkFolders/CreatBulkFolders/example_input_2_book_wise.txt. In this mode all folders with same parent name will be created inside that parent folder. 

The input file and the output folders will be present in the same folder in which the program is present.


