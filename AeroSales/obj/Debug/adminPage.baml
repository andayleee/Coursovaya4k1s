   M S B A M L   `   `   `  ????D  @AeroSales, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:AeroSales	AeroSales  5          AeroSales.adminPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:AeroSales     5        N? Title$   Администратор??5	      .??5      ?  ? RowDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6    ? ColumnDefinitions 5   
   ?? 5      $
??0.5* ?6   ?? 5      $??* ?6   ?? 5       $
??0.5* ?6   .+?5"   
   ?? $??1??6   $??0??6$   $??15??61   ???#0c73fe6C   ?? 5#   
   $??0??6   $??3??6$   $??0??69   ???#ff6f326F   ?? 5$   
   $??1??6   $??0??6    $??0 0 0 0q?6-   $ ??АДМИНИСТРАТОР??6>   $??Center=?6V   $??Center=?6z   $??30(?6?   $??Syncopate Cyr)?6?   
???????6?   ?? 5%   
   -   5        $??btnBack??5%   $   
???   ?$??Yu Gothic UI Light)?63   ???#ff6f32$??0??6h   $??0??6x   $
??🠔??6?   $??40(?6?   $??0q?6?   $??60??6?   $
??Left=?6?   6?   ?? 5&   
   $??1??6   $??1??6'   $??14??64   .??5'   
   ?? $??1??6   $??1??6%   $??14??62   .+?5(      ?? -   5        $??btnPost??5(   5   
???????$??40??6(   $??Century Gothic)?6D   ???#0c73fe$??1??6u   $??1??6?   $??Должность??6?   $??25(?6?   $??0q?6?   6?   ?? 5)      -   5        $??btnEmployee??5)   4   
???????$??40??6(   $??Century Gothic)?6G   ???#0c73fe$??1??6x   $??2??6?   $??Сотрудник??6?   $??25(?6?   $??0q?6?   6?   ?? 5*      -   5        $??btnTicketList??5*   4   
???????$??40??6(   $??Century Gothic)?6I   ???#0c73fe$??1??6z   $??3??6?   $!??Список билетов??6?   $??25(?6?   $??0q?6?   6?   ?? 5+      -   5        $??btnFlightList??5+   4   
???????$??40??6(   $??Century Gothic)?6I   ???#0c73fe$??1??6z   $??4??6?   $??Список рейсов??6?   $??25(?6?   $??0q?6?   6?   ?? 5,      -   5        $??	btnFlight??5,   4   
???????$??40??6(   $??Century Gothic)?6E   ???#0c73fe$??1??6v   $??5??6?   $??Рейс??6?   $??25(?6?   $??0q?6?   6?   ?? 5-      -   5        $??btnContract??5-   4   
???????$??40??6(   $??Century Gothic)?6G   ???#0c73fe$??1??6x   $??6??6?   $??Контракт??6?   $??25(?6?   $??0q?6?   6?   ?? 5.      -   5        $??btnFinanciatReport??5.   4   
???????$??40??6(   $??Century Gothic)?6N   ???#0c73fe$??1??6   $??7??6?   $%??Финансовый отчет??6?   $??25(?6?   $??0q?6?   6?   ?? 5/      -	   5        $??btnTaxReturn??5/   4   
???????$??40??6(   $??Century Gothic)?6H   ???#0c73fe$??1??6y   $??8??6?   $-??'Налоговая декларация??6?   $??25(?6?   $??0q?6?   6?   ?? 50      -
   5        $??btnCompanyAccount??50   4   
???????$??40??6(   $??Century Gothic)?6M   ???#0c73fe$??1??6~   $??9??6?   $!??Счета компании??6?   $??25(?6?   $??0q?6?   6?   ?? 51      -   5        $??btnGraf??51   5   
???????$??40??6(   $??Century Gothic)?6D   ???#0c73fe$??1??6u   $??10??6?   $??График??6?   $??25(?6?   $??0q?6?   6?   ?? 52      -   5        $??btnRezervnoye??52   4   
???????$??40??6(   $??Century Gothic)?6I   ???#0c73fe$??1??6z   $??11??6?   $/??)Резервное копирование??6?   $??25(?6?   $??0q?6?   6?   ?? 53      -   5        $??	btnBackUp??53   4   
???????$??40??6(   $??Century Gothic)?6E   ???#0c73fe$??1??6v   $??12??6?   $??BackUp??6?   $??25(?6?   $??0q?6?   6?   54      55      56      57      