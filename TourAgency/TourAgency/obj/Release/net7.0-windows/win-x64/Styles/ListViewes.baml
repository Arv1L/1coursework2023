   M S B A M L   `   `   `  ���� E  ATourAgency, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:TourAgency
TourAgency  W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   UserListView  �� 
TargetType �� Property  AlmostWhite �� Value �� 
TargetType    TourAgency.ClippingBorder �� CornerRadius J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth   Opacity	    Child
 J� SnapsToDevicePixels  UserItemContainer  AlmostBlack
 � Tag �� Triggers P� Property P� Value  
LightGreen  SeaGreen  TourListView  TourListViewItem  	LightGray � ColumnDefinitions   Name 		 Green �� Orientation ��Name �� Padding �� Binding �� Value �� Triggers �� 
TargetName�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     "localclr-namespace:TourAgency  %�  &	        &	 �     &	 \    &	 �    �� "  ��.���  � ��# �  ��  � ��$ 0q���  � �� �� " ��.R�   $ 10��#���     $ 5Q�$ 0Q�$ .5Q�.	 �� $|�false��#��z��.���� #
 z
 �� "  ��.���  � ��$ 30����  � ��# �  ��  � ��# �  ��  � ��$ Regular"���  � ��$ 0q���  � �� �� .R��� #��z��._��� #��z #��z��$��Center=�$
��10 0q� P�  �@J�IsMouseOver$
 True��. ���  � ��# �  ��  � ��# �  ��  � ��$ DemiBold"�P�  �@��
IsSelected$
 True��. ���  � ��# �  ��  � ��# �  ��  � ��$ DemiBold"��� "  ��.���  � ��$ Disabled=���  � ��$ Disabled=��� "  ��.���  � ��$ 0q���  � ��# �  ��  � ��# �  ��  � ��$ 0q���  �@�OverridesDefaultStyle$
 True����  � ��$	 0 1q���  � �� �� " ��.R�� $��95�� �� $��5 ��� $	��235 �.+�   $ border��$��1��$��Bottom=����� "  ��.���  �@��CornerRadius$ 	20 0 0 20����  � ��# � 	 .	 � $��90��.+��� $��Center=�$��Center=�.+��� $��20(�$
��Bold"�$��Bottom=�$��Center=����� *City+�� $��12(�$��Bottom=�$��Center=����� *	Country+�� $ 
Horizontal=�$��Bottom=�$��Right=�$��0 0 5 5q�.+��� $��12(����� *Price+�� $��грн��$��12(�$��5 0 0 0q��� $ soldOut��$ 5��$	��Top=�$
��Left=�
��� E��$ 5q�$��2������ "  ��.���  � y�$ Hidden=� ��  �� *CurrentTicketsNumber+ 0.a���  � y�$ Visible=�._��� $��SOLD OUT��#���  $��12(�$
��Bold"� P�  �@J�IsMouseOver$
 True��P�  �@��
IsSelected$
 True��. ��� $ border��# �   � ���� # �   � ��