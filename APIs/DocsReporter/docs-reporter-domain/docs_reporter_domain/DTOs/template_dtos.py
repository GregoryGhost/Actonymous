from dataclasses import dataclass
from typing import final


@final
@dataclass
class ContractInfo:
    approval_date: str = ""
    """Approval date, for example (родительный падеж):
        <<15>> августа 2022 г."""
    number: str = ""
    """Document number (day-month-year), for example: 15-08-22"""


@final
@dataclass
class WorkInfo:
    exact_work_cost: str = ""
    """Exact work cost, for example: 322 228.666"""
    exact_comma_work_cost: str = ""
    """Exact work cost, for example: 322 228, 666"""
    work_cost: str = ""
    """Work cost, for example: 322 228"""
    work_cost_in_words: str = ""
    """Work cost in words (винительный падеж), for example:
        322 228 (триста двадцать две тысячи двести двадцать восемь) рублей 00 коп."""
    total_spent_hours: float = 0.0
    """Total spent hours, for example: 176.5"""


@final
@dataclass
class ContractsInfo:
    act: ContractInfo
    contract: ContractInfo
    supplementary_agreement: ContractInfo
    work_info: WorkInfo


@final
@dataclass
class InclinedInfo:
    nominative: str = ""
    genitive: str = ""
    dative: str = ""
    accusative: str = ""
    instrumental: str = ""
    prepositional: str = ""


@final
@dataclass
class CompanyInfo:
    header_fullname: InclinedInfo
    header_fullname_initials_in_end: InclinedInfo
    header_fullname_initials_in_front: InclinedInfo
    header_position: InclinedInfo
    company_name: str
    initial_company_name: str


@final
@dataclass
class ContractPartiesInfo:
    customer: CompanyInfo
    executor: CompanyInfo