   M S B A M L   `   `   `  ????D  @AeroSales, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:AeroSales	AeroSales  5      "    AeroSales.restorPasswordPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      X TXceed.Wpf.Toolkit, Version=4.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      #localclr-namespace:AeroSales  5        N? Title$  restorPasswordPage??5
      .??5      ? ???#0c73fe ? RowDefinitions 5   
   ?? 5      $??2* ?6   ?? 5      $??* ?6   ?? 5      $
??0.1* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $
??0.1* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6    ? ColumnDefinitions 5   
   ?? 5      $
??0.1* ?6   ?? 5       $??0.45* ?6   ?? 5!      $
??0.1* ?6   ?? 5"      $??0.45* ?6   ?? 5#      $
??0.1* ?6   ?? 5$      $
??0.1* ?6   ?? 5%      $??0.45* ?6   ?? 5&      $
??0.1* ?6   ?? 5'      $??0.45* ?6   ?? 5(      $
??0.1* ?6   .+?5*   
   ?? 
???????$??5??6!   $??5??61   $??15??6E   ?? 5+   
   ???#ff6f32$??0??6#   $??10??63   $??60??6H   $	??Top=?6T   $??0??6l   ?? 5,   
   $??10??6   $??0??6%   $??0??65   $??0 0 0 0q?6B   $1??+ВОССТАНОВЛЕНИЕ ДОСТУПА??6S   $??60??6t   $	??Top=?6?   $??Center=?6?   $??Center=?6?   $??30(?6?   $??Syncopate Cyr)?6?   
???????6  ?? 5-   
   -   5        $??btnBack??5-   $   
???   ?$??Yu Gothic UI Light)?63   $	??Top=?6S   ???#ff6f32$??0??6?   $??2??6?   $??0??6?   $
??🠔??6?   $??40(?6?   $??0q?6?   $??60??6?   $??60??6?   $
??Left=?6?   6  ?? 5/   
   $??3??6   $??1??6$   $??1??64   $??0 0 0 0q?6A   $!??НОМЕР ТЕЛЕФОНА??6R   $??Center=?6k   $??Center=?6?   $??30(?6?   $??Syncopate Cyr)?6?   
???????6?   %  Xceed.Wpf.Toolkit.MaskedTextBox  50   
   -   5        $??	txtNumber??50   I      Mask$ +7(000)000-00-00??6   $??3??65   $??0q?6Z   $??Center=?6o   $??Center=?6?   
?????? 
???????$??1??6?   $??3??6?   $??5 0 5 0q?6  $??30(?6  ?? 52   
   $??3??6   $??1??6$   $??5??64   $??0 0 0 0q?6A   $??КОДОВОЕ СЛОВО??6R   $??Center=?6j   $??Center=?6?   $??30(?6?   $??Syncopate Cyr)?6?   
???????6?   ?? 53   
   -   5        $??txtCodeWord??53   &   $??3??6   $??0q?69   $??Center=?6N   $??Center=?6r   
?????? 
???????$??1??6?   $??7??6?   $??5 0 5 0q?6?   $??30(?6?   ?? 55   
   $??3??6   $??1??6$   $??9??64   $??0 0 0 0q?6A   $??EMAIL??6R   $??Center=?6b   $??Center=?6?   $??30(?6?   $??Syncopate Cyr)?6?   
???????6?   ?? 56   
   -   5        $??txtEmail??56   &   $??3??6   $??0q?66   $??Center=?6K   $??Center=?6o   
?????? 
???????$??1??6?   $??10??6?   $??5 0 5 0q?6?   $??30(?6?   ?? 58   
   $??3??6   $??1??6$   $??8??64   $??0 0 0 0q?6A   $a??[Введите кодовое слово, указанное при регистрации.??6R   $??Center=?6?   $??15(?6?   $	??Top=?6?   $??Century Gothic)?6?   
???   ?6?   ?? 59   
   $??3??6   $??1??6$   $??11??64   $??0 0 0 0q?6B   $M??GВведите email, указанный при регистрации.??6S   $??Center=?6?   $??15(?6?   $	??Top=?6?   $??Century Gothic)?6?   
???   ?6?   ?? 5:   
   $
??Wrap=?6   $??3??6(   $??12??6<   $??6??6N   $??2??6^   $??0 0 0 0q?6k   $??25(?6~   $??Century Gothic)?6?   
???   ?usЕсли вам не удается войти в аккаунт, следуйте инструкциям ниже.?? 6?   ?? 6
  hf1.Введите требуемые данные для восстановления аккаунта.?? 6N  ?? 6[  752.Проверьте введенные данные.?? 6?  ?? 6?  ??Обратите внимание, что если вы уже пытались восстановить доступ к аккаунту, но получили сообщение 'Нам не удалось убедиться, что этот аккаунт принадлежит вам', попробуйте ещё раз.?? 6S  ?? 6`  ??Если восстановить доступ к аккаунту так и не удалось, обратитесь к администратору по номеру тел.: +7(999)999-99-996?  ?? 5;   
   -   5        $??
btnRestore??5;   $   
???????$??0q?66   $??Syncopate Cyr)?6J   ???#ff6f32$??1??6z   $??3??6?   $??12??6?   $??5 5 5 5q?6?   $+??%ВОССТАНОВИТЬ ДОСТУП??6?   $??25(?6?   6?   5=      5>      