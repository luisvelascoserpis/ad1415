using System;

namespace PReflection
{
	[Entity(TableName = "category")]
	public class Categoria {
		public Categoria(ulong id, string nombre) {
			this.id = id;
			this.nombre = nombre;
		}

		[Id]
		private ulong id;
		[NotBlank]
		private string nombre;

		public ulong Id { 
			get { return id; }
			set { id = value; }
		}

		public string Nombre { 
			get { return nombre; }
			set { nombre = value; }
		}
	}
}

