   M S B A M L   `   `   `  ����G  CAWS_DynamoDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AWS_DynamoDBAWS_DynamoDB  5   	       AWS_DynamoDB.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:AWS_DynamoDB     5   	     � Title$  
MainWindow��5   	   $	��380��6   $	��426��6)    � WindowStartupLocation$ CenterScreen=�65   .��5	      � $��	0,0,0,-16q�6   .+�5
   
   �� $%��Lab#2 - Amazon DynamoDB Service��6   $��Center=�6:   $��
0,91,0,211q�6W   $	��226��6k   $
��Bold"�6w   �� 5   
   $��/img/DynamoDB.png��6   $��Center=�6+   $��74��6H   $	��Top=�6T   $��74��6l   $��0,10,0,0q�6w   �� 5   
   $��Email:��6   $
��Left=�6!   $��
71,138,0,0q�6<   $	��Top=�6P   �� 5   
   -   5         ��Name$ 
txtBxEmail��5      $
��Left=�6&   $��144,138,0,0q�6A    �� TextWrapping$
 Wrap=�6V   $	��Top=�6j   $	��167��6�   $��26��6�   �� 5   
   $��
Password: ��6   $
��Left=�6%   $��
71,182,0,0q�6@   $	��Top=�6T   G� 5   
   -   5         G�Name$ pwBxPassword��5      $
��Left=�6,   $��144,182,0,0q�6G   $	��Top=�6\   $	��167��6t   $��26��6�   �� 5   
   -   5         ��Name$ btnLogin��5   5    �� 	IsDefault$
 True��6   
�������$��Login��6G   $
��Left=�6W   $��144,226,0,0q�6r   $	��Top=�6�   $��31��6�   $	��167��6�   6�   �� 5   
   
�������$��Center=�6!   $��40��6>   $��	0,302,0,0q�6J   
���   �$	��Top=�6l   $	��426��6�   �� 5   
   $��Kenneth General��6   $
��Left=�6*   $��
10,309,0,0q�6E   $	��Top=�6Y    J� RenderTransformOrigin$ -0.153,-0.008&�6q   �� 5   
   -   5         ��Name$ lblValidation��5   !   
���  ��$�� ��68   $
��Left=�6C   $��144,271,0,0q�6^   $	��Top=�6s   $	��167��6�   5      5      