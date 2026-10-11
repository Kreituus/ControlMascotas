// =====================================================================
//  SISTEMA DE CONTROL DE MASCOTAS - SPRINT 1
//  Programación I (SIS-1121) - Universidad Privada Domingo Savio
//  Alcance Sprint 1: una mascota, sin arreglos ni funciones propias.
//  Contenidos: variables, tipos de datos, operadores, secuencia,
//  if / if-else / switch, for / while / do-while, contador y acumulador.
// =====================================================================
using System;
using System.Globalization;

namespace ControlMascotas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // ----- Constantes -----
            const int EDAD_MIN = 0;
            const int EDAD_MAX = 30;
            const int MAX_CONTROLES = 5;

            // ----- Datos de la mascota (HU01) -----
            string nombreMascota = "";
            string especie = "";
            int edad = 0;
            bool mascotaRegistrada = false;

            // ----- Datos del propietario (HU02) -----
            string nombrePropietario = "";
            string telefono = "";
            bool propietarioRegistrado = false;

            // ----- Controles de peso (HU04 / HU05) -----
            int contadorControles = 0;      // contador
            double sumaPesos = 0;           // acumulador
            double pesoMaximo = 0;
            double pesoMinimo = 0;

            int opcion = 0;
            string entrada;

            // ===== HU07 - Menú principal (do-while + switch) =====
            do
            {
                Console.WriteLine();
                Console.WriteLine("=================================");
                Console.WriteLine("  SISTEMA DE CONTROL DE MASCOTAS");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Registrar mascota");
                Console.WriteLine("2. Registrar propietario");
                Console.WriteLine("3. Mostrar datos");
                Console.WriteLine("4. Registrar controles de peso");
                Console.WriteLine("5. Calcular peso promedio");
                Console.WriteLine("6. Clasificar etapa de vida");
                Console.WriteLine("7. Salir");
                Console.Write("Elija una opción: ");
                entrada = Console.ReadLine();
                if (entrada == null) break;              // fin de entrada
                if (!int.TryParse(entrada, out opcion)) opcion = 0;

                switch (opcion)
                {
                    // ===== HU01 - Registrar mascota =====
                    case 1:
                        Console.WriteLine("\n--- REGISTRAR MASCOTA ---");

                        // Nombre: no puede estar vacío (while)
                        Console.Write("Nombre de la mascota: ");
                        nombreMascota = (Console.ReadLine() ?? "").Trim();
                        while (nombreMascota.Length == 0)
                        {
                            Console.Write("  El nombre no puede estar vacío. Ingrese de nuevo: ");
                            nombreMascota = (Console.ReadLine() ?? "").Trim();
                        }

                        // Especie: se elige con switch
                        int opEspecie = 0;
                        do
                        {
                            Console.Write("Especie (1=Perro, 2=Gato, 3=Ave, 4=Otro): ");
                            int.TryParse(Console.ReadLine(), out opEspecie);
                            switch (opEspecie)
                            {
                                case 1: especie = "Perro"; break;
                                case 2: especie = "Gato"; break;
                                case 3: especie = "Ave"; break;
                                case 4: especie = "Otro"; break;
                                default: Console.WriteLine("  Opción de especie inválida."); break;
                            }
                        } while (opEspecie < 1 || opEspecie > 4);

                        // Edad: entero entre EDAD_MIN y EDAD_MAX (while)
                        Console.Write("Edad en años (" + EDAD_MIN + "-" + EDAD_MAX + "): ");
                        while (!int.TryParse(Console.ReadLine(), out edad) || edad < EDAD_MIN || edad > EDAD_MAX)
                        {
                            Console.Write("  Edad inválida. Ingrese un número entre " + EDAD_MIN + " y " + EDAD_MAX + ": ");
                        }

                        mascotaRegistrada = true;
                        contadorControles = 0;   // nueva mascota: se reinician los controles
                        sumaPesos = 0;
                        Console.WriteLine(">> Mascota \"" + nombreMascota + "\" registrada correctamente.");
                        break;

                    // ===== HU02 - Registrar propietario =====
                    case 2:
                        Console.WriteLine("\n--- REGISTRAR PROPIETARIO ---");
                        if (!mascotaRegistrada)
                        {
                            Console.WriteLine("Primero debe registrar una mascota (opción 1).");
                        }
                        else
                        {
                            Console.Write("Nombre del propietario: ");
                            nombrePropietario = (Console.ReadLine() ?? "").Trim();
                            while (nombrePropietario == "")
                            {
                                Console.Write("  El nombre no puede estar vacío. Ingrese de nuevo: ");
                                nombrePropietario = (Console.ReadLine() ?? "").Trim();
                            }

                            // Teléfono: exactamente 8 dígitos (for recorre cada carácter)
                            bool telefonoValido = false;
                            while (!telefonoValido)
                            {
                                Console.Write("Teléfono (8 dígitos): ");
                                telefono = (Console.ReadLine() ?? "").Trim();
                                telefonoValido = telefono.Length == 8;
                                for (int i = 0; i < telefono.Length && telefonoValido; i++)
                                {
                                    if (telefono[i] < '0' || telefono[i] > '9')
                                        telefonoValido = false;
                                }
                                if (!telefonoValido)
                                    Console.WriteLine("  El teléfono debe tener exactamente 8 dígitos.");
                            }
                            propietarioRegistrado = true;
                            Console.WriteLine(">> Propietario registrado para " + nombreMascota + ".");
                        }
                        break;

                    // ===== HU03 - Mostrar datos =====
                    case 3:
                        Console.WriteLine("\n--- DATOS REGISTRADOS ---");
                        if (!mascotaRegistrada)
                        {
                            Console.WriteLine("No hay mascotas registradas.");
                        }
                        else
                        {
                            Console.WriteLine("Mascota     : " + nombreMascota);
                            Console.WriteLine("Especie     : " + especie);
                            Console.WriteLine("Edad        : " + edad + " año(s) (" + (edad * 12) + " meses)");
                            if (propietarioRegistrado)
                            {
                                Console.WriteLine("Propietario : " + nombrePropietario);
                                Console.WriteLine("Teléfono    : " + telefono);
                            }
                            else
                            {
                                Console.WriteLine("Propietario : (sin registrar)");
                            }
                            Console.WriteLine("Controles   : " + contadorControles + " control(es) de peso");
                        }
                        break;

                    // ===== HU04 - Registrar controles de peso (for + contador + acumulador) =====
                    case 4:
                        Console.WriteLine("\n--- CONTROLES DE PESO ---");
                        if (!mascotaRegistrada)
                        {
                            Console.WriteLine("Primero debe registrar una mascota (opción 1).");
                        }
                        else
                        {
                            int cantidad;
                            Console.Write("¿Cuántos controles registrará? (1-" + MAX_CONTROLES + "): ");
                            while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad < 1 || cantidad > MAX_CONTROLES)
                            {
                                Console.Write("  Cantidad inválida. Ingrese entre 1 y " + MAX_CONTROLES + ": ");
                            }

                            contadorControles = 0;
                            sumaPesos = 0;
                            for (int i = 1; i <= cantidad; i++)
                            {
                                double peso;
                                Console.Write("Peso del control " + i + " (kg): ");
                                string texto = (Console.ReadLine() ?? "").Replace(',', '.');
                                while (!double.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out peso) || peso <= 0 || peso > 150)
                                {
                                    Console.Write("  Peso inválido (mayor a 0 y hasta 150 kg). Ingrese de nuevo: ");
                                    texto = (Console.ReadLine() ?? "").Replace(',', '.');
                                }

                                if (contadorControles == 0)
                                {
                                    pesoMaximo = peso;
                                    pesoMinimo = peso;
                                }
                                else
                                {
                                    if (peso > pesoMaximo) pesoMaximo = peso;
                                    if (peso < pesoMinimo) pesoMinimo = peso;
                                }
                                sumaPesos += peso;        // acumulador
                                contadorControles++;      // contador
                            }
                            Console.WriteLine(">> Se registraron " + contadorControles + " controles de peso.");
                        }
                        break;

                    // ===== HU05 - Calcular peso promedio =====
                    case 5:
                        Console.WriteLine("\n--- PESO PROMEDIO ---");
                        if (contadorControles == 0)
                        {
                            Console.WriteLine("No hay controles de peso registrados (opción 4).");
                        }
                        else
                        {
                            double promedio = sumaPesos / contadorControles;
                            Console.WriteLine("Total acumulado : " + sumaPesos.ToString("0.00", CultureInfo.InvariantCulture) + " kg");
                            Console.WriteLine("Controles       : " + contadorControles);
                            Console.WriteLine("Peso promedio   : " + promedio.ToString("0.00", CultureInfo.InvariantCulture) + " kg");
                            Console.WriteLine("Peso mínimo     : " + pesoMinimo.ToString("0.00", CultureInfo.InvariantCulture) + " kg");
                            Console.WriteLine("Peso máximo     : " + pesoMaximo.ToString("0.00", CultureInfo.InvariantCulture) + " kg");
                            if (pesoMaximo - pesoMinimo > 1)
                                Console.WriteLine("Observación     : variación de peso mayor a 1 kg, revisar con el veterinario.");
                            else
                                Console.WriteLine("Observación     : peso estable.");
                        }
                        break;

                    // ===== HU06 - Clasificar etapa de vida (if / else if / else) =====
                    case 6:
                        Console.WriteLine("\n--- ETAPA DE VIDA ---");
                        if (!mascotaRegistrada)
                        {
                            Console.WriteLine("No hay mascotas registradas.");
                        }
                        else
                        {
                            string etapa;
                            if (edad < 1)
                                etapa = "Cachorro / Cría";
                            else if (edad <= 7)
                                etapa = "Adulto";
                            else
                                etapa = "Senior";

                            Console.WriteLine(nombreMascota + " (" + especie + ", " + edad + " año(s)) -> " + etapa);
                            if (etapa == "Senior")
                                Console.WriteLine("Recomendación: control veterinario cada 6 meses.");
                            else if (etapa == "Adulto")
                                Console.WriteLine("Recomendación: control veterinario anual.");
                            else
                                Console.WriteLine("Recomendación: completar calendario de vacunas.");
                        }
                        break;

                    case 7:
                        Console.WriteLine("\nGracias por usar el Sistema de Control de Mascotas.");
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        break;
                }
            } while (opcion != 7);
        }
    }
}
