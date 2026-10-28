# Tareas — v1.0

**Regla:** implementar únicamente lo definido en `spec.md`. No anticipar v1.1 ni v1.2.

* [x] **T01 — Revisar documentación:** comprobar consistencia entre `requirements.md`, `spec.md` y `plan.md`; señalar cualquier ambigüedad antes de programar.
* [x] **T02 — Preparar solución:** crear la solución para Visual Studio y la estructura Entidades–Controlador–Formularios (.NET Framework 4.7.2).
* [x] **T03 — Generar entidad:** crear `Producto` con los cinco datos establecidos.
* [x] **T04 — Generar controlador:** desarrollar `TLista` con registro, modificación y eliminación, validaciones y resultados claros (EF01–EF03).
* [x] **T05 — Generar formulario:** diseñar `frmProductos` sencillo y funcional. Convertir las entradas según `spec.md` y delegar las reglas de negocio en `TLista`. Permitir solo selección por código para modificar/eliminar, sin consultas generales, filtros ni búsquedas avanzadas.
* [x] **T06 — Crear pruebas automáticas:** usar MSTest V2 según `plan.md` para comprobar CA01–CA12 y CA15 sobre el controlador, sin abrir el formulario. Restaurar el estado inicial de cada caso y variante; comprobar que los demás productos se conservan y registrar resultados verificables.
* [ ] **T07 — Compilar y verificar manualmente:** comprobar el targeting pack 4.7.2 y compilar la solución con MSBuild. Abrir y ejecutar la aplicación en Visual Studio; verificar CA13–CA14, los mensajes, el precio con coma o punto de CA01 y la selección por código. Registrar resultados esperados y obtenidos, y corregir errores.
* [ ] **T08 — Automatizar:** crear `.github/workflows/build.yml` en `windows-2022` según `plan.md`: comprobar o instalar el Developer Pack 4.7.2, restaurar paquetes, compilar con MSBuild y ejecutar las pruebas con VSTest y el adaptador MSTest V2. Conservar resultados TRX y fallar ante errores o ausencia de pruebas. Comprobar que el pipeline finalice correctamente.
* [ ] **T09 — Integrar v1.0:** registrar commits y crear un Pull Request obligatorio con revisión registrada. Etiquetar `v1.0` solo después de integrar el PR, con compilación, pruebas automáticas, revisión manual y pipeline aprobados.

## Verificación de la versión 1.0

* Distinguir las pruebas ejecutadas, con su resultado y evidencia, de las pendientes por limitaciones del entorno, indicando la limitación concreta. No considerar aprobada una prueba pendiente.
* Ejecutar las pruebas automatizadas del controlador para CA01–CA12 y CA15, y las pruebas manuales del formulario indicadas en T07; registrar la cobertura de CA01–CA15.
* Corregir los errores encontrados sin modificar los requerimientos establecidos.
* Comprobar que el proyecto compile correctamente en Visual Studio.
* Verificar la ejecución del pipeline de GitHub Actions.
* Registrar e integrar los cambios mediante un Pull Request.


## Estado de implementación y verificación

T01–T06 completadas. T07 parcial: solución compilada con MSBuild de Visual Studio Insiders y formulario abierto; pruebas interactivas pendientes por fallo del control de ventanas. T08 parcial: workflow configurado, pendiente de ejecución en GitHub Actions. T09 pendiente por instrucción del usuario: sin commits, push, Pull Request ni etiqueta.

Los resultados ejecutados y pendientes se detallan en [VERIFICACION-v1.0.md](VERIFICACION-v1.0.md).