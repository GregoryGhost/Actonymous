import requests
import json
import sys
from dataclasses import dataclass
from typing import final


@final
@dataclass
class CostInfo:
    cost: float = 0.0
    unit: str = "рубль"


@final
@dataclass
class InclinedCost:
    cost: str = ""
    unit: str = ""


InclinedCostResult = InclinedCost | None


def incline_cost(cost_info: CostInfo) -> InclinedCostResult:
    try:
        url = "https://ws3.morpher.ru/russian/spell"
        headers = {"User-Agent": "My Python script"}
        number_str = str(cost_info.cost)
        params = dict(
            n=number_str,
            unit=cost_info.unit,
            format="json",
            # token= "AUTH TOKEN HERE"
        )

        response = requests.get(url=url, params=params, headers=headers)
        is_failed_response = response.status_code != 200
        if is_failed_response:
            print(
                f"Failed response({response.status_code}) to morpher on do incline cost",
                file=sys.stderr,
            )

            return None

        data = json.loads(response.text)
        cost = data.get("n").get("В")
        unit = data.get("unit").get("В")
        inclined_cost = InclinedCost(cost=cost, unit=unit)

        return inclined_cost
    except Exception:
        print("Unknown error on do incline cost", file=sys.stderr)

        return None