   M S B A M L   `   `   `  ����@  <Views, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewsViews  5   	       Views.Login   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Views     5   	     � Title$  Login��5   	   $	��450��6   $	��800��6$    � WindowStyle$
 None=�60    � 
ResizeMode$ NoResize=�6D    � WindowStartupLocation$ CenterScreen=�5	   	   
������  � AllowsTransparency .  � 	Resources 5      3  -System.Windows.Media.Effects.DropShadowEffect "  MaterialDesignElevationShadow2   
BlurRadius   	Direction   ShadowDepth	   Color��%e   &	          5   
   $ 15Q�5      $	 270Q�5      $ 3Q�5      $	 #CCCCCC��5      5      .��5      �� 
 �� CornerRadius$
 10��6     BackgroundPrimaryBrush#��[ 5        BackgroundTertiaryBrush#��[ 5      $��1q�5       J� Effect# �   5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��1.2* �6#   �� 5      $��* �6#   .+�5"      �� -   5         ��Name$ 	btnCerrar��5"      $��X��6(   $��30��5#      $��30��5$      $��0q�5&      
���  ��  TextSecondaryBrush#��[ 5(      $��1��6B   $��333,0,0,402q�6R   6g   �� 5*      $��0��6     PrimaryBrush#��[ 5+      $
 	10 0 0 10��5,      ._�5-      � .+�5.      �� $��Center=�6!   $��Center=�5/      $��20q�50      .+�51      A� -   5         A�Name$ FotoUsuario��51   "   $	��160��67   ���	#CCF7DDDD$��5��6W   $	��160��6k    J� RenderTransformOrigin$ 0.5,0.5&�6w   ��52      �� 53   "    �� ImageSource$ /Images/Logo.png��6-   z�55      V� 56   "   .~�57   &   �� �� 58   &   �� 59   &    �� Angle$ -0.698Q�66   T� 5:   &   5;   #   5=      �� 5>      $��SIGUE��6$   
�������$��24(�5@   "   $
��Bold"�5A   "    �� TextAlignment$ Center=�5B   "   �� 5C      $$��Sistema de Gestión Académica��6$   
�������$��16(�5E   "   $ Center=�5F   "   $��0 10 0 0q�5G   "   5H      5I      5J      �� 5M      $��1��6   $
��30,0q�5N      $��Center=�5O      .+�5S      �� $��
Bienvenido��6   $ Center=�5T        TextPrimaryBrush#��[ 5U      $��28(�5V      $
��Bold"�5W      $��0 0 0 30q�5X      �� 5[      $��Usuario��6   #��[ 5\      $��0 0 0 5q�5]      �� 5^        BackgroundSecondaryBrush#��[ 6   $
 5��5_      $��0 0 0 15q�5`      ._�5a      �� -   5         ��Name$ 
txtUsuario��5a      $��0q�62   $��10q�5b      
������ $��40��5d      
�������6l   5e      �� 5h      $��Contraseña��6   #��[ 5i      $��0 0 0 5q�5j      �� 5k      #��[ 6   $
 5��5l      $��0 0 0 20q�5m      ._�5n      G� -   5         G�Name$ psbPassword��5n   "   $��0q�67   $��10q�5o   !   
������ $��40��5q   !   
�������6p   5r      �� 5w      -   5        $ btnIngresar��5w      $��Iniciar Sesión��6/     SecondaryBrush#��[ 5x      
�������$��40��5z      $��0q�5{      $��0 0 0 15q�5|      6+   5      5�      5�      5�      