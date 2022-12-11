from dataclasses import dataclass
import sys
from dataclasses_json import dataclass_json, Undefined
from typing import Optional, final

from ..DTOs import InclinedInfo


@dataclass_json
@dataclass
class FullnameInfo:
    Ф: str = ""
    И: str = ""
    О: str = ""


@dataclass_json
@dataclass
class InclinedMorpherDeclensionMany:
    И: str = ""
    Р: str = ""
    Д: str = ""
    В: str = ""
    Т: str = ""
    П: str = ""


@dataclass_json
@dataclass
class InclinedMorpherInfo:
    Р: str = ""
    Д: str = ""
    В: str = ""
    Т: str = ""
    П: str = ""
    ФИО: Optional[FullnameInfo] = None
    множественное: Optional[InclinedMorpherDeclensionMany] = None


def get_inclined_info(data: InclinedMorpherInfo, nominative: str) -> InclinedInfo:
    inclined_info = InclinedInfo(
        nominative=nominative,
        genitive=data.Р,
        dative=data.Д,
        accusative=data.В,
        instrumental=data.Т,
        prepositional=data.П,
    )

    return inclined_info


import requests

Fullname = str

HeaderPosition = str

InclinedFullname = InclinedInfo

InclinedHeaderPosition = InclinedInfo

InclinedFullnameResult = InclinedFullname | None


def unwrap_inclined_header_position(
    header_position: HeaderPosition,
    default_value: InclinedFullname = InclinedFullname(),
) -> InclinedHeaderPosition:
    return unwrap_inclined_fullname(header_position, default_value)


def unwrap_inclined_fullname(
    fullname: Fullname, default_value: InclinedFullname = InclinedFullname()
) -> InclinedFullname:
    inclined_fullname = incline_fullname(fullname)
    result = inclined_fullname or default_value

    return result


def incline_fullname(fullname: Fullname) -> InclinedFullnameResult:
    try:
        url = "https://ws3.morpher.ru/russian/declension"
        headers = {"User-Agent": "My Python script"}
        params = dict(
            s=fullname,
            format="json",
            # token= "AUTH TOKEN HERE"
        )

        response = requests.get(url=url, params=params, headers=headers)
        is_failed_response = response.status_code != 200
        if is_failed_response:
            print(
                f"Failed response({response.status_code}) to morpher on do incline fullname",
                file=sys.stderr,
            )

            return None

        data = InclinedMorpherInfo.schema().loads(response.text)
        inclined_fullname = get_inclined_info(data, fullname)

        return inclined_fullname
    except Exception:
        print("Unknown error on do incline fullname", file=sys.stderr)

        return None