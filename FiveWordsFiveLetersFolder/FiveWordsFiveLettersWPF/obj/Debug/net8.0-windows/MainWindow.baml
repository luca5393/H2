   M S B A M L   `   `   `  ����  FiveWordsFiveLettersWPF(    "FiveWordsFiveLettersWPF.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5      �  � RowDefinitions 5	   
   �� 5
      $
��Auto �6   $��65��6*   �� 5      �� 5      $
��Auto �6   $��62��6*   �� 5      $
��Auto �6   $��56��6*   �� 5      $
��Auto �6   $��126.08��6*    � ColumnDefinitions 5   
   �� 5      $	��49* �6   �� 5      $
��Auto �6   $	��300��6,   �� 5      $	��51* �6   .+�5   
   �� -   5        $��Select File��5      $��0��6'   $��1��64   $��10,10,10,10q�6D   6Y   �� 5   
   -   5         ��Name$ FilePathTextBox��5      $��1��6+   $��3��68   $��10,10,10,10q�6L   6a   �� 5   
   -   5        $��StartButton��5      $��Start��6$   $��2��64   $��	0,10,0,10q�6A   $��Center=�6n   $	��226��6�   �� 5   
   -   5         ��Name$ 
SaveButton��5      $��Save Result��6%   $��2��6;   $��2��6H   $��10,10,10,10q�6X   ${�False��6�   � 5   
   -   5        $��ProgressBar��5      $��3��6)   $��3��66   $��10,10,10,10q�6J   �� 5   
   -   5        $��
StatusText��5      $��4��6&   $��Center=�63   $��	0,10,0,10q�6P   $��14(�6c   $��1��6q   $	��280��6�   5       5!      