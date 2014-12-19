package serpis.ad;

public class Vector {

	public static void main(String[] args) {
		
		int[] vector = new int[] {7, 16, 12, 15, 19};
		int minValue = min(vector);
		System.out.println("minValue=" + minValue);
		
	}
	
	public static int min(int[] v) {
		int minValue = v[0];
//		for (int index = 1; index < v.length; index++)
//			if (v[index] < minValue)
//				minValue = v[index];
		for (int item : v)
			if (item < minValue)
				minValue = item;
		
		return minValue;
	}

}
