   M S B A M L   `   `   `  ????D  @AeroSales, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:AeroSales	AeroSales  5          AeroSales.contractPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=4.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      #localclr-namespace:AeroSales  5        N? Title$  contractPage??5
      .??5      ?  ? RowDefinitions 5   
   ?? 5      $
??0.7* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.2* ?6   ?? 5      $
??0.3* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5      $
??0.5* ?6   ?? 5       $
??0.5* ?6   ?? 5!      $
??0.5* ?6   ?? 5"      $
??0.5* ?6   ?? 5#      $
??0.5* ?6   ?? 5$      $
??0.5* ?6   ?? 5%      $
??0.5* ?6   ?? 5&      $
??0.5* ?6   ?? 5'      $
??0.5* ?6   ?? 5(      $
??0.5* ?6   ?? 5)      $
??0.5* ?6   ?? 5*      $
??0.5* ?6   ?? 5+      $
??0.5* ?6   ?? 5,      $
??0.5* ?6   ?? 5-      $
??0.5* ?6   ?? 5.      $
??0.5* ?6   ?? 5/      $
??0.5* ?6    ? ColumnDefinitions 51   
   ?? 52      $
??0.1* ?6   ?? 53      $
??0.9* ?6   ?? 54      $
??0.9* ?6   ?? 55      $
??0.1* ?6   ?? 56      $
??0.1* ?6   ?? 57      $
??0.9* ?6   ?? 58      $
??0.9* ?6   ?? 59      $
??0.1* ?6   ?? 5:      $??* ?6   ?? 5;      $??* ?6   ?? 5<      $??* ?6   ?? 5=      $??* ?6   .+?5?   
   ?? $??2??6   $??2??6$   $??1??65   $??2??6E   $??0 0 0 0q?6R   $'??!Дата формирования??6c   $??Center=?6   $??Center=?6?   $??18(?6?   $??Yu Gothic UI Light)?6?   (  "System.Windows.Controls.DatePicker  5@   
   -   5        $??
DatePicker??5@      $??2??6'   $??3??6;   $??0q?6L   $??Yu Gothic UI Light)?6`   $??Center=?6?   $??Center=?6?   
?????? 
???????$??1??6?   $??4??6  $??5 0 5 0q?6  $??20(?6%  ?? 5B   
   $??2??6   $??2??6$   $??5??65   $??2??6E   $??0 0 0 0q?6R   $F??@Наименование компании перевозчика??6c   $??Center=?6?   $??Center=?6?   $??18(?6?   $??Yu Gothic UI Light)?6?   ?? 5C   
   -   5        $??	txtNameCo??5C      $??2??6#   $??3??67   $??0q?6H   $??Yu Gothic UI Light)?6\   $??Center=?6|   $??Center=?6?   
?????? 
???????$??5??6?   $??4??6  $??20(?6  &   System.Windows.Controls.DataGrid  5E   
   -   5        $	??dg1??5E      $??8??6   $??5??6.   $??11??6;   $??29??6P   $??14(?6b      
IsReadOnly . 6?   ?? 5G   
   -   5        $??btnBack??5G   $   
???   ?$??Yu Gothic UI Light)?63   
???????$??0??6f   $??2??6v   $??2??6?   $??0??6?   $
??🠔??6?   $??40(?6?   $??0q?6?   $??60??6?   $
??Left=?6?   6?   ?? 5I   
   -   5        $??	txtSearch??5I      $??4??6#   $??2??67   $??0q?6H   $??Yu Gothic UI Light)?6\   $??Center=?6|   
?????? 
???????$??8??6?   $??1??6?   $??0 0 0 0q?6?   $??20(?6?   6  ?? 5J   
   -   5        $??	btnSearch??5J      $??Yu Gothic UI Light)?6"   $??0 0 0 15q?6B   $??3??6T   ???#0c73fe$??8??6z   $??3??6?   $??
Поиск??6?   $??20(?6?   $??0q?6?   ?? 5K   
   -   5        $??	btnInsert??5K      $??Yu Gothic UI Light)?6"   $??0 0 0 15q?6B   $??3??6T   ???#0c73fe$??9??6{   $??3??6?   $??Добавить??6?   $??20(?6?   $??0q?6?   6?   ?? 5L   
   -   5        $??	btnUpdate??5L      $??Yu Gothic UI Light)?6"   $??0 0 0 15q?6B   $??3??6T   ???#0c73fe$??10??6{   $??3??6?   $??Изменить??6?   $??20(?6?   $??0q?6?   6?   ?? 5M   
   -	   5        $??	btnDelete??5M      $??Yu Gothic UI Light)?6"   $??0 0 0 15q?6B   $??3??6T   ???#0c73fe$??11??6{   $??3??6?   $??Удалить??6?   $??20(?6?   $??0q?6?   6?   5N      5O      