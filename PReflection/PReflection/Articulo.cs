using System;

namespace PReflection
{
	[Entity]
	public class Articulo {
		public ulong Id { get ; set; }
		public string Nombre { get; set; }
		public Categoria Categoria { get; set;}
		public decimal Precio { get; set;}
	}
}

