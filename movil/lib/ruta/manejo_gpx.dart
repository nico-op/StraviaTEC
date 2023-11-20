import 'dart:io';
<<<<<<< Updated upstream
=======

import 'package:file_saver/file_saver.dart';
>>>>>>> Stashed changes

abstract class Coordenadas {
  double get latitude;
  double get longitude;
}

class GoogleMapsLatLng implements Coordenadas {
  final double latitude;
  final double longitude;

  GoogleMapsLatLng(this.latitude, this.longitude);
}

class LatLongLatLng implements Coordenadas {
  final double latitude;
  final double longitude;

  LatLongLatLng(this.latitude, this.longitude);
}

class GPXHelper {
  static const String folderName = 'mi_carpeta';

  String generateGPXContent(List<Coordenadas> routePoints) {
    // Aquí debes reemplazar con tu lógica para generar el contenido GPX
    final StringBuffer gpxContent = StringBuffer();

    gpxContent
        .writeln('<?xml version="1.0" encoding="UTF-8" standalone="no" ?>');
    gpxContent.writeln(
        '<gpx version="1.1" xmlns="http://www.topografix.com/GPX/1/1">');

    for (Coordenadas point in routePoints) {
      gpxContent
          .writeln('  <wpt lat="${point.latitude}" lon="${point.longitude}">');
      gpxContent.writeln('    <name>Waypoint</name>');
      gpxContent.writeln('    <desc>Details about the waypoint</desc>');
      gpxContent.writeln('  </wpt>');
    }

    return gpxContent.toString();
  }

  Future<void> saveGPXToFile(List<Coordenadas> routePoints) async {
    try {
      // Solicitar directorio al usuario
      String? selectedDirectoryPath = await getDirectoryPath();

      // Solicitar nombre de archivo al usuario
      String fileName = await inputFileNameFromUser();

      // Generar contenido GPX
      String gpxContent = generateGPXContent(routePoints);

      // Construir ruta de archivo
      String filePath = '$selectedDirectoryPath/$fileName';

      // Guardar archivo
      File file = File(filePath);
      await file.writeAsString(gpxContent);

      print('Archivo GPX guardado en: $filePath');
    } catch (e) {
      print('Error guardando archivo GPX: $e');
    }
  }

  Future<String> getDirectoryPath() async {
    // Remover initialFileName ya que no es un parámetro válido para saveFile
    return await FileSaver.instance.saveFile(name: '');
  }

  Future<String> inputFileNameFromUser() async {
    print('Ingresa el nombre del archivo GPX:');
    String? name = stdin.readLineSync();
    return name ?? 'straviaTEC_route.gpx';
  }
}
