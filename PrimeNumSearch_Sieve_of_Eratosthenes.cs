/*
	C#

	It's a C#LanguageProgram

	2014/09/05
*/

using System;
using System.Diagnostics; 
using System.Threading;
using System.IO;

// Mainメソッドを持つクラス
public class PrimeNumSearch_Sieve_of_Eratosthenes
{

//	private int cntmax=1000000;
//	private int cnt=0;
//	public bool[] prime = new bool[1000000];

	static void Main()
	{
		Stopwatch sw = new Stopwatch();	// New StopWatch();
		sw.Start();		// Timer Start
		Search_Sieve_of_Eratosthenes();
		sw.Stop();		// Timer END

        Console.WriteLine("計測を完了しました");          
        Console.WriteLine("経過時間の合計 = {0}", sw.Elapsed);
        Console.WriteLine("ミリ秒単位の経過時間の合計 = {0}", sw.ElapsedMilliseconds); 
        Console.WriteLine("経過時間の分解能 (1秒あたり) = {0}", Stopwatch.Frequency);
        

    }

	static void Search_Sieve_of_Eratosthenes()
	{
		// variable
		int cntmax=10000000;
		int cnt=0;
		bool[] prime = new bool[cntmax];

		// format variable
	 	prime[0] = false;
	 	prime[1] = false;
	 	// まず最初に配列を初期化.
		for(int i=2; i<cntmax; i++)
		{
			prime[i]=true;
		}
		// ここからエラトステネスの篩が始まります.
		// 探索をするのはcntmaxの平方根までで良いので.
		// for分の条件式は "(i*i)<cntmax"となっている.
		for(int i=2; (i*i)<cntmax; i++)
		{
			if(prime[i] == true)
			{
				for(int y=(i << 1); y<cntmax; y+=i)
				{
					if(prime[y] == true)
					{
						prime[y] = false;
						cnt++;
					}
				}
			}
		}

		// ここで配列のtrue(素数)を大放出！.
		WriteFile(prime,cntmax);
//		for(int i=2; i<cntmax; i++)
//		{
//			if(prime[i] == true)	Console.WriteLine(i);
//		}

		// 630 ごとに改行を打つように素数を出力する
//		for (int i=2;i<6300;i++)
//		{
//			if(prime[i] == true)	Console.WriteLine(i);
//			if(i % 630 == 0)	Console.WriteLine("\n");
//		}
	}

	static void WriteFile(bool[] primes,int cntmaxs)
	{
//		if (args.Length == 0) {
//			Console.WriteLine("ファイル名を指定してくださいね。");
//			return;
//		}

		FileStream fs;
		int[] primekazu = new int[cntmaxs/630];

		try
		{
			// ファイルストリームを作成する
			fs = new FileStream("PrimeNumText.txt", FileMode.Create);
		}
		catch (Exception e)
		{
			// 例外なら終了する
			Console.WriteLine("ファイルを新規作成できません！");
			Console.WriteLine(e.Message);
			return;
		}

		// ファイルストリームをStreamWritterに関連付ける
		StreamWriter sw = new StreamWriter(fs);

		// 630 ごとに改行を打つように素数を PrimeNumText に出力する
		for (int i=2,j=0,jcnt=0;i<cntmaxs;i++)
		{
//			if(primes[i] == true)	sw.WriteLine(i);
//			if(i % 630 == 0)	sw.WriteLine("\n");
			if(primes[i] == true)
			{
				j++;
				sw.Write("{0},",i);
			}
			if(i % 630 == 0){
				primekazu[jcnt] = j;
				j=0;
				jcnt++;
//				sw.Write("\n");
			}
		}

		sw.Write("\n");


		for (int i=0;i<cntmaxs/630;i++)
		{
			sw.Write("{0},", primekazu[i]);
//			if(i % 90 == 0)	sw.Write("\n");
		}

		// 終了処理を行う
		sw.Close();
		fs.Close();
	}
}
