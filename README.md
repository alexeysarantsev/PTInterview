# PTInterview
�������
��� ���������� �������� ���� � ������ ����� ���������, �������� ������ ����� ���������.
������ ������� ����� ������������������ �����:
������ �������������� ������
������ ���������� ������� N(i)
������ ��������� N(i+1) = N(i-1) + N(i). ��������� ������� ���������� � ������ ����������.
������ ����������� �����������
� ��� �� ��������� ����������.
������ ���������� ��� ������ �������� �������� � ����� �����, ������� ����������� �������� ������. ��� ������� ���� �����������.


 �����������

 1. ��� ��� ������������ ������������� �����, ��� ����� ���������� ��������� ����������� �����, ������ ��� �� ���� ������ �������� ����� ������ �����.
 2. �� ���� � ������ �������� ������ N-��� �����. ����� ��������� F(N+1), ��� ����� ����� � F(N-1). �� �� ����� ������ ��� �������� F(N-1) ���� F(N), ������ F(N-1) �� ���������� ��������. ��� �����, ������� � ���������� �������� F(N-2), �� ������ ��������� � F(N-1) � ������������, ����� ���� ������� � ������� F(N)
 3. �� �� ����� ���������� ���������� ����� �����, ����� �������� �������.
 4. �� ������ ���� ������ � ����, ��� ��������� ������ ����� "�����������". �� �� ������ ������������ �� ��, ��� ���� �� �������� F(N), �� ��� ������� �� ������� F(N-2)
 5. ���� ��������� ������� ����������. ���� �� ������ ����������� F(N+1) �� F(N), �� ������� ��� ��� F(N) ��� ��� ������ ������� ��� ���, ��������� ����� ��� ��. ����� ������ ���������, ����� ��������� ������ �����.

���������� ����������

���������� ���������� ��������� � ������ FiboCalculator (FiboApp2.Application.Services, FiboApp1.Application.Services). � �������� ���������� ����������� ����������, �� ������ ��������� �������������.
��� ����������� ���������� (�.1) � ��������� ConcurrentDictionary<BigInteger, BigInteger>. ���� - F(N), �������� - F(N+1).
��� ����������� F(N-1) (�.2) � ��������� ConcurrentDictionary<BigInteger, BigInteger>. ���� - F(N+2), �������� - F(N+1).
��� �������� ������������ �����, ��� ������� �� ���-�� ����������, � ��������� ConcurrentQueue<BigInteger>. ����� ������ ������� ��������� ��������� ��������, � ������ ��� ��� �������, ��� � ����, ���� ������ ������� �� ��������� ������� ��������.
��� ���������� �.5 � ��������� ConcurrentDictionary<BigInteger, object>, ��� �������� - ��� ������ ������, �� �������� �� ������ lock. � ����� ������������ SemaphoreSlim (Wait/Release ) ������ lock, �� �� ������ ����������� � ��������.

���� ��� ����������, ��� ��� F(N) ��� ������������ F(N-1), �� ����� ������ GetNextFromScratch, ������� ����� ������� ����� � ������ ������.

���������� �������� ������
��� � ������������� � ������� - ������ ���������� ����� �� ������ ����� web api, ������ � ������ - ����� RabbitMQ

������
� ������� ���� docker-compose.yml ��� ������� RabbitMq. �� ������ ���� ������� ������ 
����� ����������� FiboApp2, � ��������� ������� - FiboApp1.
� appsettings.json ������� FiboApp1 ���� ��������� ParallelComputations ������������ ���������� ������������ �������
FiboApp1 �����, ��� �������, ���������� ������ ����� (0) �� ������ ������.

