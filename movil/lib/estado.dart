import 'package:flutter/material.dart';
import 'actividades.dart';

class ActivityProvider with ChangeNotifier {
  final List<Actividad> _actividades = [];

  List<Actividad> get actividades => _actividades;

  void addActivity(Actividad activity) {
    _actividades.add(activity);
    notifyListeners();
  }
}
