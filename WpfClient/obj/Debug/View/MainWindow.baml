   M S B A M L   `   `   `  ����A  =Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/$clr-namespace:Common;assembly=CommonCommon  5      G CWpfTcpClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:WpfTcpClient.ViewModelWpfTcpClient.ViewModel 5         WpfTcpClient.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ;
converters$clr-namespace:Common;assembly=Common     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:WpfTcpClient.ViewModel     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$  
MainWindow��5	      $	��800��5
      $	��450��5       � DataContext 5      *  $WpfTcpClient.ViewModel.MainViewModel  5   
    � 	Resources 5         DisabledBrush �� 
StartPoint �� EndPoint � Color � Offset   Common.DateFormatter  DateFormatter��%�   &	        &	 �     �� 5   
   $ 0.5,0&�64   $ 0.5,1&�6G   .��5      � $ Black��6   � 5      $ 1Q�6   $ 	LightGray��6&   5        5   
   6#   5      .��5      � #��[  6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      �� 5      �� 5      �� 5      �� 5       $
��auto �6   .+�5"   
   �� $��0��5#      $��10q�5$      $��Right=�5%      
����  �$��4q�5'       �� CornerRadius$ 10��5(      ._�5)      �� -   5        $��logBut��5*      $��Login��5,      $��18(�5-      $
��Bold"�5.      ��5+      �� *LoginCommand+5.      5/      �� 50   
   $��1��51      $��3��52      $��10q�53      
�������
���  ��$��4q�56      $ 10��57      ._�58      &   System.Windows.Controls.DataGrid  	   AutoGenerateColumns	 .  
   GridLinesVisibility$

 None=�5:         HeadersVisibility$
 None=�5;      ��5<      �� *AllMessages+   Columns 5=      4  .System.Windows.Controls.DataGridTemplateColumn  5>         CellTemplate 5?      �� 5@      .R�5A   "   �� ��6,   ��  �� Path$ DateTime� �� 	Converter# [ 5B      5D        5E       5F      �� 5G      .R�5H   "   �� ��6,   �� $ 	User.Name�5I      5K        5L       5M      �� 5N      .R�5O   "   �� ��6,   �� $
 Text�5P      5R      5T      5U      �� 5V   
   $��4��5W      $��10q�5X      
���  ��$��4q�5Z      $ 10��5[      ._�5\      �� ��6   �� $ UserMessage.Text�5]      �� 5^   
   $��5��5_      $��Center=�5`       �� Orientation$ 
Horizontal=�5a      .+�5b      �� $��10q�5c      
����  �$��4q�5e      $ 10��5f      ._�5g      �� $��Отправить��5i      $��18(�5j      $
��Bold"�5k      ��5h      �� *AddMesageCommand+5k      5l      5m      5n      5o      