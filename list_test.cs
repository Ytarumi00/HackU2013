using System;
using System.Collections.Generic;

public class test_list{
	struct point{
		public int x;
		public int y;
	}
	private List<point> lst = new List<point>();
	public void set_point(int X,int Y){
		point temp;
		temp.x = X;
		temp.y = Y;
		lst.Add(temp);
	}
	public int get_pointX(int i){
		return this.lst[i].x;
	}

	public int get_pointY(int i){
		return this.lst[i].y;
	}

	public int get_count(){
		return this.lst.Count;
	}

	
}

class Progranm{
	public static void Main(){
		test_list Mouse = new test_list();
		test_list Eye = new test_list();
		for(int i = 0;i < 10;i++){
			Mouse.set_point(i,20-i);
		}
		Console.WriteLine(" < Mouse > ");
		Console.WriteLine("elements: {0}",Mouse.get_count());
		Console.WriteLine("contents: ");
		for(int i = 0;i < Mouse.get_count();i++){
			Console.WriteLine("({0},{1})",Mouse.get_pointX(i),Mouse.get_pointY(i));
		}
		for(int i = 0;i < 10;i++){
			Eye.set_point(i*10,20-i*10);
		}
		Console.WriteLine(" < EYE > ");
		Console.WriteLine("elements: {0}",Eye.get_count());
		Console.WriteLine("contents: ");
		for(int i = 0;i < Eye.get_count();i++){
			Console.WriteLine("({0},{1})",Eye.get_pointX(i),Eye.get_pointY(i));
		}

	}
}
