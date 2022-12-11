from docs_reporter_domain import (
    MappedUserWorklogs,
    UserWorklogItemDto,
    incline_cost,
    CostInfo,
    WorkInfo,
    ContractInfo,
    incline_fullname,
    ContractsInfo,
    CompanyInfo,
    unwrap_inclined_fullname,
    unwrap_inclined_header_position,
    ContractPartiesInfo,
    JinjaTemplates,
    get_task_doc_template,
    get_act_doc_template,
    JinjaTemplate,
    ActJinjaTemplate,
    ITemplateInfo,
    ActData,
    TaskJinjaTemplate,
    TaskData
)


def get_jira_worklogs() -> MappedUserWorklogs:
    worklogs_str = '[{"task_name":"Что есть зло? Чтобы это не было, оно возникает из слабости","task_code":"D2C-1","time_spent_seconds":52200,"start_period_date":1662987567.499,"end_period_date":1663078476.16},{"task_name":"Требуется много таланта и умения, чтобы скрыть свой талант и умение","task_code":"D2C-2","time_spent_seconds":126000,"start_period_date":1662473799.184,"end_period_date":1663047038.527},{"task_name":"Человек - это животное, которое умеет совершать сделки. Ни одна собака не будет меняться костью с другой собакой","task_code":"D2C-3","time_spent_seconds":225000,"start_period_date":1661522651.363,"end_period_date":1662722506.429},{"task_name":"Мы не должны расстраиваться, когда другие скрывают правду от нас, так как мы скрываем правду и от себя тоже","task_code":"D2C-4","time_spent_seconds":115200,"start_period_date":1660831746.787,"end_period_date":1661497110.483},{"task_name":"У каждого человека внутри сидит предатель","task_code":"D2C-10","time_spent_seconds":99000,"start_period_date":1660573388.942,"end_period_date":1660813309.804}]'
    worklogs = UserWorklogItemDto.schema().loads(worklogs_str, many=True)

    return worklogs


def zpad(val: float, n: int) -> str:
    bits = str(val).split(".")
    result = "%s" % (bits[0].zfill(n))

    return result


def fmt_number_by_spaces(val: float) -> str:
    result = "{:,}".format(val)
    result = result.replace(",", " ")

    return result


def fmt_number_by_spaces_comma(val: float) -> str:
    result = fmt_number_by_spaces(val).replace(".", ", ")

    return result


def get_work_cost_in_words(exact_work_cost: float) -> str:
    inclined_cost = incline_cost(CostInfo(cost=exact_work_cost))
    if inclined_cost is None:
        return ""

    pennies = int(exact_work_cost % 1 * 100)
    pennies = zpad(pennies, 2)
    formatted_work_cost = fmt_number_by_spaces(int(exact_work_cost))
    result = f"{formatted_work_cost} ({inclined_cost.cost}) {inclined_cost.unit} {pennies} коп."

    return result


def get_total_comma_work_cost(val: float) -> str:
    result = fmt_number_by_spaces_comma(val)
    result = f"{result} руб. НДС не облагается"

    return result


def get_work_info() -> WorkInfo:
    exact_work_cost = 322610.89
    formatted_exact_work_cost = fmt_number_by_spaces(exact_work_cost)
    work_cost = int(exact_work_cost)
    work_cost_in_words = get_work_cost_in_words(exact_work_cost)
    total_spent_hours = 171.5
    formatted_work_cost = fmt_number_by_spaces(work_cost)
    formatted_exact_comma_work_cost = get_total_comma_work_cost(exact_work_cost)
    work_info = WorkInfo(
        exact_work_cost=formatted_exact_work_cost,
        exact_comma_work_cost=formatted_exact_comma_work_cost,
        work_cost=formatted_work_cost,
        work_cost_in_words=work_cost_in_words,
        total_spent_hours=total_spent_hours,
    )

    return work_info


def get_act_info() -> ContractInfo:
    month_name = "сентябрь"
    inclined_month = incline_fullname(month_name)
    inclined_month = (
        inclined_month.genitive if inclined_month is not None else f"{month_name}(!)"
    )
    approval_date = f"<<14>> {inclined_month} 2022 г."
    doc = ContractInfo(approval_date=approval_date, number="14-09-22")

    return doc


def get_supplementary_agreement_info() -> ContractInfo:
    month_name = "август"
    inclined_month = incline_fullname(month_name)
    inclined_month = (
        inclined_month.genitive if inclined_month is not None else f"{month_name}(!)"
    )
    approval_date = f"<<15>> {inclined_month} 2022 г."
    doc = ContractInfo(approval_date=approval_date, number="15-08-22")

    return doc


def get_contract_info() -> ContractInfo:
    month_name = "июль"
    inclined_month = incline_fullname(month_name)
    inclined_month = (
        inclined_month.genitive if inclined_month is not None else f"{month_name}(!)"
    )
    approval_date = f"<<14>> {inclined_month} 2022 г."
    doc = ContractInfo(approval_date=approval_date, number="14-07-22")

    return doc


def get_contracts_info() -> ContractsInfo:
    act = get_act_info()
    contract = get_contract_info()
    supplementary_agreement = get_supplementary_agreement_info()
    work_info = get_work_info()
    contracts_info = ContractsInfo(
        act=act,
        contract=contract,
        supplementary_agreement=supplementary_agreement,
        work_info=work_info,
    )

    return contracts_info


def get_customer() -> CompanyInfo:
    header_fullname_initials_in_end = unwrap_inclined_fullname("Киетака А.Х.")
    header_fullname_initials_in_front = unwrap_inclined_fullname("А.Х. Киетака")
    header_position = unwrap_inclined_header_position("Генеральный директор")
    company_name = "<<Класс превосходства>>"
    initial_company_name = f"ООО {company_name}"
    info = CompanyInfo(
        header_fullname=header_fullname_initials_in_end,
        header_fullname_initials_in_end=header_fullname_initials_in_end,
        header_fullname_initials_in_front=header_fullname_initials_in_front,
        header_position=header_position,
        company_name=company_name,
        initial_company_name=initial_company_name,
    )

    return info


def get_executor() -> CompanyInfo:
    header_fullname = "Ягами Лайт Саятиро"
    header_fullname_initials_in_end = unwrap_inclined_fullname("Ягами Л.С.")
    header_fullname_initials_in_front = unwrap_inclined_fullname("Л.С. Ягами")
    header_position = unwrap_inclined_header_position("ИП")
    company_name = header_fullname
    initial_company_name = company_name
    info = CompanyInfo(
        header_fullname=header_fullname_initials_in_end,
        header_fullname_initials_in_end=header_fullname_initials_in_end,
        header_fullname_initials_in_front=header_fullname_initials_in_front,
        header_position=header_position,
        company_name=company_name,
        initial_company_name=initial_company_name,
    )

    return info


def get_contract_parties_info() -> ContractPartiesInfo:
    customer = get_customer()
    executor = get_executor()
    info = ContractPartiesInfo(customer=customer, executor=executor)

    return info


def get_doc_templates() -> JinjaTemplates:
    return [get_act_doc_template(), get_task_doc_template()]


def get_template_info(
    template: JinjaTemplate,
    worklogs: MappedUserWorklogs,
    contracts_info: ContractsInfo,
    contract_parties_info: ContractPartiesInfo,
) -> ITemplateInfo:
    is_act_template = isinstance(template, ActJinjaTemplate)
    if is_act_template:
        template_info = ActData(
            doc_template=template,
            contracts_info=contracts_info,
            worklogs=worklogs,
            contract_parties_info=contract_parties_info,
        )

        return template_info

    is_task_template = isinstance(template, TaskJinjaTemplate)
    if is_task_template:
        template_info = TaskData(
            doc_template=template,
            contracts_info=contracts_info,
            worklogs=worklogs,
            contract_parties_info=contract_parties_info,
        )

        return template_info

    raise Exception("Unknown template type. Provide matching for this template type.")
